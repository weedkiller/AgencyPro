// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.OrganizationPeople.Models;
using AgencyPro.Core.OrganizationPeople.ViewModels;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.People.Enums;
using Omu.ValueInjecter;
using System;
using System.Linq;

namespace AgencyPro.Data.Repositories
{
    public static class OrganizationPersonRepository
    {
        public static OrganizationPerson GetById(this IQueryable<OrganizationPerson> people,
            Guid organizationId, Guid personId)
        {
            return people.First(x => x.PersonId == personId && x.OrganizationId == organizationId);
        }

      
        public static OrganizationPerson CreateOrgPerson(this IRepositoryAsync<OrganizationPerson> repo, OrganizationPersonInput input, Guid organizationId)
        {
            var entity = new OrganizationPerson
            {
                Created = DateTimeOffset.UtcNow,
                Updated = DateTimeOffset.UtcNow,
                PersonId = input.PersonId,
                OrganizationId = organizationId,
                IsOrganizationOwner = input.IsCustomer,
                IsDefault = true,
                IsDeleted = false,
                Status = PersonStatus.Active,
                ObjectState = ObjectState.Added,
                AffiliateCode = Guid.NewGuid()
                    .ToString()
                    .Substring(0,5)
                    .ToUpper(),
                Customer = input.IsCustomer ? new OrganizationCustomer()
                {
                    CustomerId = input.PersonId,
                    OrganizationId = organizationId,
                    IsDefault = input.IsCustomer,
                    ObjectState = ObjectState.Added
                } : null,
                Contractor = input.IsContractor ? new OrganizationContractor()
                {
                    ContractorId = input.PersonId,
                    ContractorStream = input.ContractorStream.GetValueOrDefault(),
                    OrganizationId = organizationId,
                    ObjectState = ObjectState.Added
                } : null,
                AccountManager = input.IsAccountManager ? new OrganizationAccountManager()
                {
                    AccountManagerId = input.PersonId,
                    AccountManagerStream = input.AccountManagerStream.GetValueOrDefault(),
                    OrganizationId = organizationId,
                    ObjectState = ObjectState.Added
                } : null,
                ProjectManager = input.IsProjectManager ? new OrganizationProjectManager()
                {
                    ProjectManagerId = input.PersonId,
                    OrganizationId = organizationId,
                    ProjectManagerStream = input.ProjectManagerStream.GetValueOrDefault(),
                    ObjectState = ObjectState.Added
                } : null,
                Recruiter = input.IsRecruiter ? new OrganizationRecruiter()
                {
                    RecruiterId = input.PersonId,
                    OrganizationId = organizationId,
                    RecruiterStream = input.RecruiterStream.GetValueOrDefault(),
                    RecruiterBonus = input.RecruiterBonus.GetValueOrDefault(),
                    ObjectState = ObjectState.Added
                } : null,
                Marketer = input.IsMarketer ? new OrganizationMarketer()
                {
                    MarketerId = input.PersonId,
                    OrganizationId = organizationId,
                    Created = DateTimeOffset.UtcNow,
                    MarketerStream = input.MarketerStream.GetValueOrDefault(),
                    MarketerBonus = input.MarketerBonus.GetValueOrDefault(),
                    ObjectState = ObjectState.Added
                } : null
            }.InjectFrom(input) as OrganizationPerson;
            repo.Insert(entity);
            return entity;
        }
    }
}