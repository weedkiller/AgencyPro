// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Threading.Tasks;
using AgencyPro.Core.Commission.ViewModels;
using AgencyPro.Core.OrganizationPeople.Services;

namespace AgencyPro.Core.Commission.Services
{
    public class CommissionFilters
    {

    }

    public interface ICommissionService
    {
        Task<CommissionOutput> GetCommission(IOrganizationPerson person);
    }
}
