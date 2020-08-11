// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.Leads.Models;
using AgencyPro.Core.UserAccount.Services;

namespace AgencyPro.Data.Repositories
{
    public static class LeadRepository
    {
        public static Lead GetById(this IRepositoryAsync<Lead> repo, Guid leadId)
        {
            return repo.Find(leadId);
        }

        public static int ReassignMarketer(this IRepositoryAsync<Lead> repo, IUserInfo userInfo, Lead lead, Guid marketerId)
        {
            lead.MarketerId = marketerId;
            lead.Updated = DateTimeOffset.UtcNow;
            lead.UpdatedById = userInfo.UserId;

            return repo.Update(lead);
        }

        public static int ReassignAccountManager(this IRepositoryAsync<Lead> repo, IUserInfo userInfo, Lead lead, Guid accountManagerId)
        {
            lead.AccountManagerId = accountManagerId;
            lead.Updated = DateTimeOffset.UtcNow;
            lead.UpdatedById = userInfo.UserId;

            return repo.Update(lead);
        }

    }
    public static class MarketerRepository
    {

    }
}