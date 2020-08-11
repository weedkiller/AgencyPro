// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.BuyerAccounts.Models;
using AgencyPro.Core.BuyerAccounts.Services;
using AgencyPro.Core.BuyerAccounts.ViewModels;
using AgencyPro.Core.Config;
using AgencyPro.Core.CustomerAccounts.Models;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Stripe.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Omu.ValueInjecter;
using Stripe;

namespace AgencyPro.Services.BuyerAccounts
{
    public class BuyerAccountService : Service<BuyerAccount>, IBuyerAccountService
    {
        private readonly ILogger<BuyerAccountService> _logger;
        private readonly CustomerService _customerService;
        private readonly IRepositoryAsync<OrganizationCustomer> _organizationCustomerRepository;
        private readonly AppSettings _appSettings;
        private readonly StripeSettings _stripeSettings;
        private readonly IRepositoryAsync<CustomerAccount> _customerAccountRepository;

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(BuyerAccountService)}.{callerName}] - {message}";
        }

        public async Task<BuyerAccountOutput> GetBuyerAccount(IOrganizationCustomer customer)
        {
            _logger.LogInformation(GetLogMessage("For customer: {0}; and organization: {1}"), customer.CustomerId, customer.OrganizationId);

            var y = await Repository
                .Queryable()
                .Include(x=>x.OrganizationBuyerAccount)
                .Where(x => x.OrganizationBuyerAccount.Id == customer.OrganizationId)
                .ProjectTo<BuyerAccountOutput>(ProjectionMapping)
                .FirstOrDefaultAsync();

            if (y == null)
            {
                _logger.LogInformation(GetLogMessage("No buyer account was found, creating one..."));
                var result = await PushCustomer(customer.OrganizationId, customer.CustomerId);

                _logger.LogDebug(GetLogMessage("{0} records updated in database"), result);

                if (result > 0)
                {
                    return await GetBuyerAccount(customer);
                }
            }
            else
                _logger.LogDebug(GetLogMessage("Buyer account found: {0}"), y.Id);

            return y;
        }

        public BuyerAccountService(
            IOptions<AppSettings> appSettings,
            ILogger<BuyerAccountService> logger,
            CustomerService customerService,

            IServiceProvider serviceProvider, 
            IStripeService stripeService) : base(serviceProvider)
        {
            _appSettings = appSettings.Value;
            _stripeSettings = appSettings.Value.Stripe;
            _logger = logger;
            _customerService = customerService;
            _organizationCustomerRepository = UnitOfWork.RepositoryAsync<OrganizationCustomer>();
            _customerAccountRepository = UnitOfWork.RepositoryAsync<CustomerAccount>();
        }


        public async Task<int> PullCustomer(Customer customer)
        {

            _logger.LogInformation(GetLogMessage("{Customer}"), customer.Id);

            var entity = await Repository.Queryable()
                .Include(x => x.IndividualBuyerAccount)
                .Include(x => x.OrganizationBuyerAccount)
                .Where(x => x.Id == customer.Id).FirstOrDefaultAsync();

            if (entity == null)
            {
                entity = new BuyerAccount
                {
                    Id = customer.Id,
                    ObjectState = ObjectState.Added
                };
            }
            else
            {
                entity.ObjectState = ObjectState.Modified;
                entity.Updated = DateTimeOffset.UtcNow;
            }

            if (customer.Metadata.ContainsKey("org_id"))
            {
                if (entity.OrganizationBuyerAccount == null)
                {
                    entity.OrganizationBuyerAccount = new OrganizationBuyerAccount()
                    {
                        BuyerAccountId = customer.Id,
                        Created = customer.Created,
                        Id = Guid.Parse(customer.Metadata["org_id"]),
                        ObjectState = ObjectState.Added
                    };
                }
                else
                {
                    entity.OrganizationBuyerAccount.ObjectState = ObjectState.Modified;
                    entity.OrganizationBuyerAccount.BuyerAccountId = customer.Id;
                    entity.OrganizationBuyerAccount.Id = Guid.Parse(customer.Metadata["org_id"]);
                }

            }
            else if (customer.Metadata.ContainsKey("person_id"))
            {
                if (entity.IndividualBuyerAccount == null)
                {
                    entity.IndividualBuyerAccount = new IndividualBuyerAccount()
                    {
                        BuyerAccountId = customer.Id,
                        Created = customer.Created,
                        Id = Guid.Parse(customer.Metadata["person_id"]),
                        ObjectState = ObjectState.Added
                    };
                }
                else
                {
                    entity.IndividualBuyerAccount.ObjectState = ObjectState.Modified;
                    entity.IndividualBuyerAccount.BuyerAccountId = customer.Id;
                    entity.IndividualBuyerAccount.Id = Guid.Parse(customer.Metadata["person_id"]);
                }
            }

            entity.Created = customer.Created;
            entity.IsDeleted = customer.Deleted.GetValueOrDefault();


            entity.InjectFrom(customer);

            return Repository.InsertOrUpdateGraph(entity, true);
        }

        public async Task<int> PushCustomer(Guid organizationId, Guid customerId)
        {

            _logger.LogInformation($@"Pushing customer record to stripe: org:" + organizationId + ", customer:" + customerId);

            var customer = await _organizationCustomerRepository.Queryable()
                .Include(x => x.Organization)
                .ThenInclude(x => x.OrganizationBuyerAccount)
                .Include(x => x.Customer)
                .ThenInclude(x => x.Person)
                .ThenInclude(x => x.ApplicationUser)
                .Where(x => x.OrganizationId == organizationId && x.CustomerId == customerId)
                .FirstOrDefaultAsync();

            Customer stripeCustomer = null;
            if (customer.Organization.OrganizationBuyerAccount != null)
                stripeCustomer = _customerService.Get(customer.Organization.OrganizationBuyerAccount.BuyerAccountId);

            if (stripeCustomer != null)
            {

                _logger.LogInformation($@"stripe customer found, updating customer records");

                stripeCustomer = _customerService.Update(stripeCustomer.Id, new CustomerUpdateOptions()
                {
                    Name = customer.Organization.Name,
                    Phone = customer.Customer.Person.ApplicationUser.PhoneNumber,
                    Address = new AddressOptions()
                    {
                        //Country = customer.Organization.Iso2,
                        City = customer.Organization.City,
                        Line1 = customer.Organization.AddressLine1,
                        Line2 = customer.Organization.AddressLine2,
                        PostalCode = customer.Organization.PostalCode,
                        //State = customer.Organization.StateProvince,
                    },
                    //Balance = 0,
                    Description = "",
                    Email = customer.Customer.Person.ApplicationUser.Email,
                    //InvoiceSettings = new CustomerInvoiceSettingsOptions()
                    //{
                    //    Footer = "",
                    //    //DefaultPaymentMethod = "",
                    //    CustomFields = new List<InvoiceCustomFieldOptions>()
                    //    {
                    //        new InvoiceCustomFieldOptions()
                    //        {
                    //            Name = "",
                    //            Value = "",
                    //        }
                    //    }
                    //},
                    Metadata = new Dictionary<string, string>()
                    {
                        { "org_id", customer.OrganizationId.ToString() },
                        { "person_id", customer.CustomerId.ToString() }
                    }
                });
            }
            else
            {

                stripeCustomer = _customerService.Create(new CustomerCreateOptions()
                {
                    Name = customer.Organization.Name,
                    //Address = new AddressOptions()
                    //{
                    //    Country = customer.Organization.Iso2,
                    //    City = customer.Organization.City,
                    //    Line1 = customer.Organization.AddressLine1,
                    //    Line2 = customer.Organization.AddressLine2,
                    //    PostalCode = customer.Organization.PostalCode,
                    //    State = customer.Organization.StateProvince,
                    //},
                    Balance = 0,
                    //  Description = "",
                    Email = customer.Customer.Person.ApplicationUser.Email,
                    //InvoiceSettings = new CustomerInvoiceSettingsOptions()
                    //{
                    //    Footer = "",
                    //    //DefaultPaymentMethod = "",
                    //    CustomFields = new List<InvoiceCustomFieldOptions>()
                    //    {
                    //        new InvoiceCustomFieldOptions()
                    //        {
                    //            Name = "",
                    //            Value = "",
                    //        }
                    //    }
                    //},
                    Metadata = new Dictionary<string, string>()
                    {
                        {"org_id", customer.OrganizationId.ToString()},
                        {"person_id", customer.CustomerId.ToString()}
                    }
                });
            }

            return await PullCustomer(stripeCustomer);
        }


        public async Task<int> ImportBuyerAccounts(int limit)
        {
            _logger.LogInformation(GetLogMessage("Limit: {limit}"), limit);

            var customers = _customerService.List(new CustomerListOptions()
            {
                Limit = limit
            });
            var totals = 0;
            foreach (var customer in customers)
            {
                var returnValue = await PullCustomer(customer);
                totals += returnValue;
            }

            return totals;
        }


        public async Task<int> ExportCustomers(Guid organizationId)
        {
            _logger.LogInformation(GetLogMessage("Exporting Customers for Org: {0}"), organizationId);

            var accounts = _customerAccountRepository.Queryable()
                .Include(x => x.ProviderOrganization)
                .Include(x => x.BuyerOrganization)
                .ThenInclude(x => x.OrganizationBuyerAccount)
                .Where(x => x.BuyerOrganization.OrganizationBuyerAccount == null && x.ProviderOrganization.Id == organizationId)
                .ToList();

            _logger.LogInformation("Customers to export: " + accounts.Count);


            var totals = 0;
            foreach (var account in accounts)
            {
                var result = await PushCustomer(account.CustomerOrganizationId, account.CustomerId);
                totals += result;
            }

            return totals;
        }

        public async Task<int> ExportCustomers()
        {
            var accounts = _customerAccountRepository.Queryable()
                .Include(x => x.BuyerOrganization)
                .ThenInclude(x => x.OrganizationBuyerAccount)
                .Where(x => x.BuyerOrganization.OrganizationBuyerAccount == null)
                .ToList();

            var totals = 0;
            foreach (var account in accounts)
            {
                var result = await PushCustomer(account.CustomerOrganizationId, account.CustomerId);
                totals += result;
            }

            return totals;
        }


        public Task<string> GetAuthUrl(IOrganizationCustomer customer)
        {
            _logger.LogInformation(GetLogMessage("Getting Auth Url for customer {Customer}"), customer);

            var theCustomer = _organizationCustomerRepository.Queryable()
                .Include(x => x.Organization)
                .ThenInclude(x => x.OrganizationBuyerAccount)
                .Include(x => x.Customer)
                .ThenInclude(x => x.Person)
                .ThenInclude(x => x.ApplicationUser)
                .First(x =>
                    x.OrganizationId == customer.OrganizationId && x.CustomerId == customer.CustomerId);

            var dict = new Dictionary<string, string>
            {
                { "client_id", _stripeSettings.ClientId },
                { "redirect_uri", _stripeSettings.RedirectUri },
                { "scope", "read_write" },
                { "response_type","code"},
                { "state", customer.OrganizationId.ToString() },
                { "stripe_user[email]", theCustomer.Customer.Person.ApplicationUser.Email },
                { "stripe_user[first_name]", theCustomer.Customer.Person.FirstName },
                { "stripe_user[last_name]", theCustomer.Customer.Person.LastName },
                { "stripe_user[country]", theCustomer.Customer.Person.Iso2 },
                { "stripe_user[business_name]", theCustomer.Organization.Name },
                { "stripe_user[business_type]", "company" },
                // {"suggested_capabilities[]","card_payments,transfers,tax_reporting_us_1099_k" }
            };

            var url = $"https://connect.stripe.com/express/oauth/authorize{BuildQuerystring(dict)}";

            _logger.LogDebug(GetLogMessage("{Url}"), url);

            return Task.FromResult(url);

        }


        public async Task<string> GetStripeUrl(IOrganizationCustomer customer, bool isRecursive = false)
        {
            _logger.LogInformation(GetLogMessage("Getting Stripe Url for customer {Customer}"), customer);

            var theCustomer = await _organizationCustomerRepository.Queryable()
                .Include(x => x.Organization)
                .ThenInclude(x => x.OrganizationBuyerAccount)
                .Where(x =>
                    x.OrganizationId == customer.OrganizationId && x.CustomerId == customer.CustomerId && x.Organization.OrganizationBuyerAccount != null)
                .FirstOrDefaultAsync();



            if (theCustomer == null)
            {
                _logger.LogDebug(GetLogMessage("Customer not found"));

                var customerResult = await PushCustomer(customer.OrganizationId, customer.CustomerId);
                if (customerResult > 0 && !isRecursive)
                    return await GetStripeUrl(customer, true);

                throw new ApplicationException("Unable to create customer account");
            }
            else
            {
                _logger.LogDebug(GetLogMessage("Customer Found: {0}"), theCustomer.CustomerId);

            }

            var result = new LoginLinkService().Create(theCustomer.Organization.OrganizationBuyerAccount.BuyerAccountId,
                new LoginLinkCreateOptions()
                {
                    RedirectUrl = _appSettings.Urls.Flow
                });

            _logger.LogDebug(GetLogMessage("{Url}"), result.Url);

            return (result.Url);
        }


        private string BuildQuerystring(Dictionary<string, string> querystringParams)
        {
            List<string> paramList = new List<string>();
            foreach (var parameter in querystringParams)
            {
                paramList.Add(parameter.Key + "=" + parameter.Value);
            }
            return "?" + string.Join("&", paramList);
        }
    }
}
