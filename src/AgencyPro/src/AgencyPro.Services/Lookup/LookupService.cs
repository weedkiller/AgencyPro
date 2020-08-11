// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using AgencyPro.Core.Candidates.Enums;
using AgencyPro.Core.Contracts.Enums;
using AgencyPro.Core.CustomerAccounts.Enums;
using AgencyPro.Core.Invoices.Enums;
using AgencyPro.Core.Leads.Enums;
using AgencyPro.Core.Lookup.Services;
using AgencyPro.Core.Lookup.ViewModels;
using AgencyPro.Core.Orders.Model;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.People.Enums;
using AgencyPro.Core.Projects.Enums;
using AgencyPro.Core.Proposals.Enums;
using AgencyPro.Core.Stories.Enums;
using AgencyPro.Core.TimeEntries.Enums;
using AgencyPro.Core.ViewModels;

namespace AgencyPro.Services.Lookup
{
    public class LookupService : ILookupService
    {
        public LookupOutput GetAll()
        {
            var models = new LookupOutput
            {
                OrganizationTypes = GetEnumOutput<int>(OrganizationType.Buyer),
                InvoiceStatus = GetEnumOutput<byte>(InvoiceStatus.Draft),
                ContractStatus = GetEnumOutput<byte>(ContractStatus.Active),
                IntakeStatus = GetEnumOutput<int>(LeadStatus.Qualified),
                AffiliateTypes = GetEnumOutput<int>(AffiliateType.None),
                TimeStatuses = GetEnumOutput<int>(TimeStatus.Logged),
                TimeTypes = GetEnumOutput<int>(TimeType.Consulting),
                ProjectStatus = GetEnumOutput<int>(ProjectStatus.Active),
                LeadStatus = GetEnumOutput<int>(LeadStatus.New),
                CandidateStatus = GetEnumOutput<byte>(CandidateStatus.New),
                AccountStatus = GetEnumOutput<int>(AccountStatus.Active),
                ProposalStatus = GetEnumOutput<int>(ProposalStatus.Pending),
                ProposalTypes = GetEnumOutput<int>(ProposalType.Ongoing),
                StoryStatus = GetEnumOutput<int>(StoryStatus.Pending),
                PersonStatus = GetEnumOutput<int>(PersonStatus.Active),
                OrderStatus = GetEnumOutput<int>(OrderStatus.Draft)
            };

            return models;
        }

        private IEnumerable<EnumOutput<T>> GetEnumOutput<T>(Enum e)
        {
            var values = Enum.GetValues(e.GetType()).Cast<object>();
            var models = values.Select(v => new EnumOutput<T>
            {
                Id = (T) v,
                Name = (v as Enum).GetDescription() ?? v.ToString()
            }).ToList();
            return models;
        }
    }
}