// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Events.OrganizationContractors;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationContractors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;

namespace AgencyPro.Services.OrganizationRoles.OrganizationContractors
{
    public partial class OrganizationContractorService
    {
        public async Task<T> Create<T>(OrganizationContractorInput model)
            where T : OrganizationContractorOutput
        {
            _logger.LogTrace(
                GetLogMessage($@"OrganizationId: {model.OrganizationId}, ContractorId: {model.ContractorId}"));

            var e = await Repository.Queryable()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.OrganizationId == model.OrganizationId && x.ContractorId == model.ContractorId);

            if (e != null)
            {
                if (e.IsDeleted)
                {
                    e.UpdatedById = _userInfo.UserId;
                    e.Updated = DateTimeOffset.UtcNow;
                    e.IsDeleted = false;
                    e.CreatedById = _userInfo.UserId;
                    e.Created = DateTimeOffset.UtcNow;
                    e.InjectFrom(model);

                    await Repository.UpdateAsync(e, true);
                }
            }
            else
            {
                e = new OrganizationContractor()
                {
                    Created = DateTimeOffset.UtcNow,
                    Updated = DateTimeOffset.UtcNow,
                    UpdatedById = _userInfo.UserId,
                    CreatedById = _userInfo.UserId,
                    ContractorStream = model.ContractorStream
                }.InjectFrom(model) as OrganizationContractor;

                await Repository.InsertAsync(e, true);
            }

            var output = await GetById<T>(model.ContractorId, model.OrganizationId);

            await Task.Run(() =>
            {
                RaiseEvent(new OrganizationContractorCreatedEvent
                {
                    OrganizationContractor = output
                });
            });

            return output;
        }

        public Task<T> Create<T>(IAgencyOwner ao, OrganizationContractorInput input)
            where T : OrganizationContractorOutput
        {
            input.OrganizationId = ao.OrganizationId;
            return Create<T>(input);
        }
    }
}