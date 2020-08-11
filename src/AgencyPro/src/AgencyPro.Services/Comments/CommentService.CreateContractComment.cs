// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.Comments.Models;
using AgencyPro.Core.Comments.ViewModels;
using AgencyPro.Core.Contracts.Extensions;
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.EntityFrameworkCore;

namespace AgencyPro.Services.Comments
{
    public partial class CommentService
    {
        private async Task<bool> CreateContractComment(Contract c, CommentInput input, Guid organizationId)
        {
            var comment = new Comment()
            {
                ContractId = c.Id,
                OrganizationId = organizationId
            };

            return await CreateComment(comment, input);
        }

        public async Task<bool> CreateContractComment(IAgencyOwner agencyOwner, Guid contractId, CommentInput input)
        {
            var contract = await ContractExtensions.ForAgencyOwner(_contractRepository.Queryable()
                    .Include(x=>x.Comments), (IProviderAgencyOwner) agencyOwner)
                .FindById(contractId)
                .FirstAsync();

            return await CreateContractComment(contract, input, agencyOwner.OrganizationId);
        }

        public async Task<bool> CreateContractComment(IOrganizationAccountManager accountManager, Guid contractId,
            CommentInput input) 
        {
            var contract = await _contractRepository.Queryable()
                .Include(x => x.Comments)
                .ForOrganizationAccountManager(accountManager)
                .FindById(contractId)
                .FirstAsync();

            return await CreateContractComment(contract, input, accountManager.OrganizationId);
        }

        public async Task<bool> CreateContractComment(IOrganizationProjectManager projectManager, Guid contractId,
            CommentInput input) 
        {
            var contract = await _contractRepository.Queryable()
                .Include(x => x.Comments)
                .ForOrganizationProjectManager(projectManager)
                .FindById(contractId)
                .FirstAsync();

            return await CreateContractComment(contract, input, projectManager.OrganizationId);
        }

        public async Task<bool> CreateContractComment(IOrganizationCustomer customer, Guid contractId,
            CommentInput input) 
        {
            var contract = await _contractRepository.Queryable()
                .Include(x => x.Comments)
                .ForOrganizationCustomer(customer)
                .FindById(contractId)
                .FirstAsync();

            return await CreateContractComment(contract, input, customer.OrganizationId);
        }

        public async Task<bool> CreateContractComment(IOrganizationContractor contractor, Guid contractId,
            CommentInput input) 
        {
            var contract = await _contractRepository.Queryable()
                .Include(x => x.Comments)
                .ForOrganizationContractor(contractor)
                .FindById(contractId)
                .FirstAsync();

            return await CreateContractComment(contract, input, contractor.OrganizationId);
       }
    }
}