// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.Organizations.ViewModels;
using Omu.ValueInjecter;
using System.Threading.Tasks;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationPeople.ViewModels;
using AgencyPro.Core.PaymentTerms.Models;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Data.Entities;
using AgencyPro.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Organizations
{
    public partial class OrganizationService
    {
        private async Task<OrganizationResult> CreateOrganization(OrganizationCreateInput input, Guid customerId)
        {
            _logger.LogInformation(GetLogMessage("Creating Organization: {0}"), input.Name );

            var organization = await Repository.Queryable()
                .Where(x => x.Name == input.Name)
                .FirstOrDefaultAsync();

            var retVal = new OrganizationResult();

            if (organization != null)
            {
                retVal.ErrorMessage = "Organization with the same name already in use";
                return retVal;
            }

            organization = new Organization
            {
                Iso2 = input.Iso2,
                ProvinceState = input.ProvinceState,
                CustomerId = customerId,
                CategoryId = 1,
                ObjectState = ObjectState.Added,
                ImageUrl = EntityConstants.DefaultOrgImageUrl,
                PrimaryColor = "grey-600",
                SecondaryColor = "grey-500",
                TertiaryColor = "grey-400",
                OrganizationType = OrganizationType.Buyer,
                PaymentTerms = new List<OrganizationPaymentTerm>()
                {
                    new OrganizationPaymentTerm()
                    {
                        IsDefault = true,
                        ObjectState = ObjectState.Added,
                        PaymentTermId = 1
                    }
                }
            }.InjectFrom(input) as Organization;

            var organizationRecords = Repository.InsertOrUpdateGraph(organization, true);

            _logger.LogDebug(GetLogMessage("{0} Organization Records updated"), organizationRecords);

            if (organizationRecords > 0)
            {
                var person = _organizationPersonRepo.CreateOrgPerson(new OrganizationPersonInput()
                {
                    PersonId = customerId,
                    IsAccountManager = false,
                    IsProjectManager = false,
                    IsContractor = false,
                    IsMarketer = false,
                    IsRecruiter = false,
                    IsCustomer = true,
                }, organization.Id);

                var result = Repository.Commit();

                if (result <= 0) return OrganizationResult.Failed;

                var records = await _buyerService.PushCustomer(person.OrganizationId, person.PersonId);
                _logger.LogDebug(GetLogMessage("{0} Records Updated"), records);

                if (records > 0)
                {
                    retVal.Succeeded = true;
                    retVal.OrganizationId = organization.Id;
                }
            }


            return retVal;
        }

        public Task<OrganizationResult> CreateOrganization(IAgencyOwner ao, OrganizationCreateInput input, Guid customerId)
        {
            return CreateOrganization(input, customerId);
        }

        public Task<OrganizationResult> CreateOrganization(ICustomer cu, OrganizationCreateInput input) 
        {
            return CreateOrganization(input, cu.Id);
        }

        public Task<OrganizationResult> CreateOrganization(IOrganizationAccountManager am, OrganizationCreateInput input, Guid customerId) 
        {
            return CreateOrganization(input, customerId);
        }
    }
}