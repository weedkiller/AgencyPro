// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Config;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.Data.UnitOfWork;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.People.Services;
using AgencyPro.Core.Stripe.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stripe;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Person = AgencyPro.Core.People.Models.Person;

namespace AgencyPro.Services.Stripe
{
    public partial class StripeService : IStripeService
    {
        private readonly IRepositoryAsync<OrganizationCustomer> _organizationCustomerRepository;
        private readonly IRepositoryAsync<Person> _personRepository;

        private readonly StripeSettings _stripeSettings;
        private readonly ILogger<StripeService> _logger;
        private readonly AppSettings _appSettings;

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[StripeService.{callerName}] - {message}";
        }
        
        public StripeService(IUnitOfWorkAsync unitOfWork,
            IOptions<AppSettings> appSettings,
            ILogger<StripeService> logger)
        {
            _appSettings = appSettings.Value;
            _stripeSettings = appSettings.Value.Stripe;
            _logger = logger;
            _personRepository = unitOfWork.RepositoryAsync<Person>();
            _organizationCustomerRepository = unitOfWork.RepositoryAsync<OrganizationCustomer>();

        }

        public Task<string> GetAuthUrl(IAgencyOwner customer)
        {
            _logger.LogInformation(GetLogMessage("Getting Auth Url for customer {Customer}"), customer);

            var theCustomer = _organizationCustomerRepository.Queryable()
                .Include(x => x.Organization)
                .ThenInclude(x => x.OrganizationFinancialAccount)
                .ThenInclude(x=>x.FinancialAccount)
                .Include(x => x.Customer)
                .ThenInclude(x => x.Person)
                .ThenInclude(x => x.ApplicationUser)
                .First(x =>
                    x.OrganizationId == customer.OrganizationId && x.CustomerId == customer.CustomerId);

            if (theCustomer.Organization.OrganizationFinancialAccount != null)
            {
                return GetStripeUrl(customer);
            }

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

        public Task<string> GetAuthUrl(IPerson person)
        {
            _logger.LogInformation(GetLogMessage("Getting Stripe Url for person {Person}"), person);


            var theCustomer = _personRepository.Queryable()
                .Include(x => x.ApplicationUser)
                .First(x => x.Id == person.Id);

            var dict = new Dictionary<string, string>
            {
                { "client_id", _stripeSettings.ClientId },
                { "redirect_uri", _stripeSettings.RedirectUri },
                { "scope", "read_write" },
                { "response_type","code"},
                { "stripe_user[email]", theCustomer.ApplicationUser.Email },
                { "stripe_user[first_name]", theCustomer.FirstName },
                { "stripe_user[last_name]", theCustomer.LastName },
                { "stripe_user[country]", theCustomer.Iso2 },
                { "stripe_user[business_type]", "individual" },
                //{"suggested_capabilities[]","transfers,tax_reporting_us_1099_misc" }

            };

            var url =  $"https://connect.stripe.com/express/oauth/authorize{BuildQuerystring(dict)}";

            return Task.FromResult(url);

        }

        public async Task<string> GetStripeUrl(IPerson person)
        {
            _logger.LogInformation(GetLogMessage("Getting Stripe Url for person {0}"), person.Id);

            var thePerson = await _personRepository
                .Queryable()
                .Include(x=>x.IndividualFinancialAccount)
                .Where(x => x.Id == person.Id)
                .FirstOrDefaultAsync();

            var financialAccountId = thePerson.IndividualFinancialAccount.FinancialAccountId;

            var result = new LoginLinkService().Create(financialAccountId,
                new LoginLinkCreateOptions()
                {
                    RedirectUrl = _appSettings.Urls.Flow
                });

            _logger.LogDebug(GetLogMessage("{Url}"), result.Url);

            return (result.Url);
        }
        
        public async Task<string> GetStripeUrl(IAgencyOwner agencyOwner, bool isRecursive = false)
        {
            _logger.LogInformation(GetLogMessage("Getting Stripe Url for agency owner: {Customer}"), agencyOwner.CustomerId);

            var theClient = await _organizationCustomerRepository.Queryable()
                .Include(x => x.Organization)
                .ThenInclude(x => x.OrganizationFinancialAccount)
                .Where(x =>
                    x.OrganizationId == agencyOwner.OrganizationId && x.CustomerId == agencyOwner.CustomerId && x.Organization.OrganizationFinancialAccount != null)
                .FirstOrDefaultAsync();

            if (theClient == null)
            {
                throw new ApplicationException("Please enable stripe account for this Organization");
            }

            var result = new LoginLinkService().Create(theClient.Organization.OrganizationFinancialAccount.FinancialAccountId,
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
