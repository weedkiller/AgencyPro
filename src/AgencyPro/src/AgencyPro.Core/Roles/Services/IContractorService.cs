// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Threading.Tasks;
using AgencyPro.Core.Common;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.Roles.ViewModels.Contractors;
using AgencyPro.Core.Services;

namespace AgencyPro.Core.Roles.Services
{
    public interface IContractorService :
        IService<Contractor>,
        IRoleService<ContractorInput, ContractorUpdateInput, ContractorOutput, IContractor>
    {
        Task<PackedList<T>> GetContractors<T>(
            IOrganizationRecruiter re, CommonFilters filters) where T : ContractorOutput;
    }
}