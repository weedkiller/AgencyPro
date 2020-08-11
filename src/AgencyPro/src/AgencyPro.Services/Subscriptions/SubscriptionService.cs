// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.Stripe.Services;
using AgencyPro.Core.Subscriptions.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AgencyPro.Core.Plans.Models;
using StripeSubscriptionItemService = Stripe.SubscriptionItemService;
using StripeSubscriptionService = Stripe.SubscriptionService;
namespace AgencyPro.Services.Subscriptions
{
    public class SubscriptionService : Service<StripeSubscription>, ISubscriptionService
    {

        private readonly SubscriptionItemService _stripeSubscriptionItemService;
        private readonly StripeSubscriptionService _subscriptionService;
        private readonly IStripeService _stripeService;
        private readonly ILogger<SubscriptionService> _logger;
        private readonly IRepositoryAsync<Organization> _organizationRepository;
        private readonly IRepositoryAsync<StripeSubscriptionItem> _itemRepository;
        private readonly IRepositoryAsync<StripePlan> _plans;

        public SubscriptionService(
            StripeSubscriptionItemService stripeSubscriptionItemService,
            StripeSubscriptionService subscriptionService,
            IStripeService stripeService,
            IServiceProvider serviceProvider,
            ILogger<SubscriptionService> logger) : base(serviceProvider)
        {
            _stripeSubscriptionItemService = stripeSubscriptionItemService;
            _subscriptionService = subscriptionService;
            _stripeService = stripeService;
            _logger = logger;
            _organizationRepository = UnitOfWork.RepositoryAsync<Organization>();
            _plans = UnitOfWork.RepositoryAsync<StripePlan>();
            _itemRepository = UnitOfWork.RepositoryAsync<StripeSubscriptionItem>();

        }

        public async Task<int> PullSubscription(Subscription subscription)
        {
            _logger.LogInformation(GetLogMessage("{Subscription}"), subscription.Id);


            var entity = await Repository.Queryable()
                .Include(x=>x.OrganizationSubscription)
                .Where(x => x.Id == subscription.Id)
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                _logger.LogDebug(GetLogMessage("this is a new subscription"));

                entity = new StripeSubscription()
                {
                    Id = subscription.Id,
                    ObjectState = ObjectState.Added,

                };


            }
            else
            {
                _logger.LogDebug(GetLogMessage("this is an existing subscription"));


                entity.ObjectState = ObjectState.Modified;
                entity.Updated = DateTimeOffset.UtcNow;
            }

            if (subscription.Metadata.ContainsKey("org_id"))
            {
                _logger.LogDebug(GetLogMessage("Org id found {0}"), subscription.Metadata["org_id"]);

                if (entity.OrganizationSubscription == null)
                {

                    _logger.LogDebug(GetLogMessage("No Organization Subscription Found"));

                    entity.OrganizationSubscription = new OrganizationSubscription()
                    {
                        Created = subscription.Created,
                        Id = Guid.Parse(subscription.Metadata["org_id"]),
                        StripeSubscriptionId = subscription.Id,
                        ObjectState = ObjectState.Added
                    };
                }
                else
                {
                    _logger.LogDebug(GetLogMessage("Organization Subscription Found"));

                    entity.OrganizationSubscription.StripeSubscriptionId = subscription.Id;
                    entity.OrganizationSubscription.ObjectState = ObjectState.Modified;
                    entity.OrganizationSubscription.Id = Guid.Parse(subscription.Metadata["org_id"]);

                }
            }

            entity.CustomerId = subscription.CustomerId;
            entity.InjectFrom(subscription);

            var subscriptionRecordsUpdated = Repository.InsertOrUpdateGraph(entity, true);

            _logger.LogDebug(GetLogMessage("Subscription Records Updated: {0}"), subscriptionRecordsUpdated);


            return subscriptionRecordsUpdated;
        }

        public string GetSubscriptionLevel(Organization organization)
        {
            switch (organization.OrganizationType)
            {
                case OrganizationType.Marketing:
                    return "marketing";
                case OrganizationType.StaffingAgency:
                    return "staffing";
                case OrganizationType.FullServiceAgency:
                    return "fullservice";
                default:
                    return "";
            }
        }


        public async Task<int> ImportSubscriptions(int limit)
        {
            _logger.LogInformation(GetLogMessage("Limit: {limit}"), limit);

            var svc = new StripeSubscriptionService();

            var subscriptions = svc.List(new SubscriptionListOptions()
            {
                Limit = limit
            });
            var totals = 0;
            foreach (var customer in subscriptions)
            {
                var returnValue = await PullSubscription(customer);
                totals += returnValue;
            }

            return totals;
        }

        public async Task<int> ExportSubscriptions()
        {
            _logger.LogInformation(GetLogMessage("Exporting Subscriptions..."));

            var orgs = _organizationRepository.Queryable()
                .Include(x => x.OrganizationSubscription)
                .Where(x => x.OrganizationSubscription == null).ToList();

            var totals = 0;
            foreach (var org in orgs)
            {
                var result = await PushSubscription(org.Id);
                totals += result;
            }

            return totals;
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[SubscriptionService.{callerName}] - {message}";
        }



        public async Task<int> ImportSubscriptionItem(SubscriptionItem item)
        {
            _logger.LogInformation(GetLogMessage("{SubscriptionItem}"), item.Id);

            var existingItem = _itemRepository.Queryable().FirstOrDefault(x => x.Id == item.Id);
            if (existingItem == null)
            {
                existingItem = new StripeSubscriptionItem()
                {
                    ObjectState = ObjectState.Added,
                    Id = item.Id
                };
            }
            else
            {
                existingItem.ObjectState = ObjectState.Modified;
            }

            existingItem.SubscriptionId = item.Subscription;
            existingItem.PlanId = item.Plan.Id;
            existingItem.IsDeleted = item.Deleted.GetValueOrDefault();
            existingItem.Quantity = item.Quantity;

            return await Task.FromResult(_itemRepository.InsertOrUpdateGraph(existingItem, true));
        }

        public async Task<int> PushSubscription(Guid organizationId, bool commit = true)
        {
            _logger.LogInformation(GetLogMessage("Push Subscription: {0}; Saving Changes: {1}"), organizationId, commit);

            var organization = await _organizationRepository
                .Queryable()
                .Include(x => x.Customers)
                .Include(x => x.OrganizationBuyerAccount)
                .Include(x => x.OrganizationSubscription)
                .Where(x => x.Id == organizationId && x.OrganizationBuyerAccount != null)
                .FirstAsync();

            _logger.LogDebug(GetLogMessage("Organization Type: {organizationType}"), organization.OrganizationType);

            if (organization.OrganizationType == OrganizationType.Buyer)
                return 0;

            Subscription stripeSubscription = null;
            if (organization.OrganizationSubscription != null)
                stripeSubscription = _subscriptionService.Get(organization.OrganizationSubscription.StripeSubscriptionId);

            if (stripeSubscription != null)
            {
                _logger.LogDebug(GetLogMessage("Found Subscription: {StripeSubscription}"), stripeSubscription.Id);

                var items = _stripeSubscriptionItemService.List(new SubscriptionItemListOptions()
                {
                    Subscription = stripeSubscription.Id
                });

                var plans = _plans.Queryable()
                    .ToDictionary(x => x.UniqueId, x => x.StripeId);

                var hasMarketingSubscription = items.Any(x => x.Plan.Id == plans["marketing"]);
                var hasRecruitingSubscription = items.Any(x => x.Plan.Id == plans["staffing"]);
                var hasProviderSubscription = items.Any(x => x.Plan.Id == plans["provider"]);

                _logger.LogDebug(GetLogMessage("Has Marketing Subscription: {0}; Has Recruiting Subscription: {1}; Has Provider Subscription: {2}"), hasMarketingSubscription, hasRecruitingSubscription, hasProviderSubscription);

                if (!hasMarketingSubscription && organization.OrganizationType.HasFlag(OrganizationType.Marketing))
                {
                    _logger.LogDebug(GetLogMessage("Needs marketing subscription"));

                    await ImportSubscriptionItem(_stripeSubscriptionItemService.Create(new SubscriptionItemCreateOptions()
                    {
                        Subscription = stripeSubscription.Id,
                        Plan = plans["marketing"]
                    }));
                }
                else
                {
                    _logger.LogDebug(GetLogMessage("Doesn't need marketing subscription"));
                }

                if (!hasRecruitingSubscription && organization.OrganizationType.HasFlag(OrganizationType.Recruiting))
                {
                    _logger.LogDebug(GetLogMessage("Needs recruiting subscription"));

                    await ImportSubscriptionItem(_stripeSubscriptionItemService.Create(new SubscriptionItemCreateOptions()
                    {
                        Subscription = stripeSubscription.Id,
                        Plan = plans["staffing"]
                    }));
                }
                else
                {
                    _logger.LogDebug(GetLogMessage("Doesn't need recruiting subscription"));
                }

                if (!hasProviderSubscription && organization.OrganizationType.HasFlag(OrganizationType.Provider))
                {
                    _logger.LogDebug(GetLogMessage("Needs provider subscription"));

                    await ImportSubscriptionItem(_stripeSubscriptionItemService.Create(new SubscriptionItemCreateOptions()
                    {
                        Subscription = stripeSubscription.Id,
                        Plan = plans["provider"]
                    }));
                }
                else
                {
                    _logger.LogDebug(GetLogMessage("Doesn't need provider subscription"));
                }

                stripeSubscription = _subscriptionService.Update(stripeSubscription.Id, new SubscriptionUpdateOptions()
                {
                    Metadata = new Dictionary<string, string>()
                    {
                        { "org", organization.Name },
                        { "org_id", organization.Id.ToString() }
                    }
                });
            }
            else
            {
                _logger.LogDebug(GetLogMessage("No subscription found"));
                
                if (!string.IsNullOrWhiteSpace(organization.OrganizationBuyerAccount.BuyerAccountId))
                {
                    _logger.LogDebug(GetLogMessage("Buyer Account Found: {0}"), organization.OrganizationBuyerAccount.BuyerAccountId);
                    
                    var plans = _plans.Queryable()
                        .ToDictionary(x => x.UniqueId, x => x.StripeId);

                    _logger.LogDebug(GetLogMessage("{@Plans}"), plans);

                    var items = new List<SubscriptionItemOptions>();
                    if (organization.OrganizationType.HasFlag(OrganizationType.Marketing))
                    {
                        _logger.LogDebug(GetLogMessage("Marketing Sub Added to items"));

                        items.Add(new SubscriptionItemOptions()
                        {
                            Plan = plans["marketing"],
                        });
                    }

                    if (organization.OrganizationType.HasFlag(OrganizationType.Recruiting))
                    {
                        _logger.LogDebug(GetLogMessage("Recruiting Sub Added to items"));

                        items.Add(new SubscriptionItemOptions()
                        {
                            Plan = plans["staffing"],
                        });
                    }

                    if (organization.OrganizationType.HasFlag(OrganizationType.Provider))
                    {
                        _logger.LogDebug(GetLogMessage("Provider Sub Added to items"));
                        items.Add(new SubscriptionItemOptions()
                        {
                            Plan = plans["provider"]
                        });
                    }

                    stripeSubscription = _subscriptionService.Create(new SubscriptionCreateOptions()
                    {
                        Customer = organization.OrganizationBuyerAccount.BuyerAccountId,
                        Items = items,
                        CollectionMethod = "charge_automatically",
                        PaymentBehavior = "allow_incomplete",
                        TrialEnd = new AnyOf<DateTime?, SubscriptionTrialEnd>(DateTime.Now.AddDays(30)),
                        Metadata = new Dictionary<string, string>()
                        {
                            { "org", organization.Name },
                            { "org_id", organization.Id.ToString() }
                        }
                    });

                    _logger.LogInformation(GetLogMessage("Stripe Subscription Created: {0}"), stripeSubscription.Id);

                }
                else
                {
                    throw new ApplicationException("No buyer account setup");
                }
            }

            return await PullSubscription(stripeSubscription);

        }

    }
}
