// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Events.OrganizationAccountManagers;
using AgencyPro.Core.OrganizationRoles.Extensions;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationAccountManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;

namespace AgencyPro.Services.OrganizationRoles.OrganizationAccountManagers
{
    public partial class OrganizationAccountManagerService
    {
        private async Task<T> Update<T>(OrganizationAccountManagerInput input) where T : OrganizationAccountManagerOutput
        {
            _logger.LogTrace(
                GetLogMessage(
                    $@"OrganizationId: {input.OrganizationId}, AccountManagerId: {input.AccountManagerId}"));

            var entity = await Repository
                .Queryable()
                .FindById(input.AccountManagerId, input.OrganizationId)
                .FirstAsync();

            entity.InjectFrom(input);
            entity.Updated = DateTimeOffset.UtcNow;

            Repository.UpdateAsync(entity, true).Wait();

            var output = await GetById<T>(input);

            await Task.Run(() =>
            {
                RaiseEvent(new OrganizationAccountManagerUpdatedEvent
                {
                    OrganizationAccountManager = output
                });
            });

            return output;
        }

        public Task<T> Update<T>(IAgencyOwner ao, OrganizationAccountManagerInput input)
            where T : OrganizationAccountManagerOutput
        {
            input.OrganizationId = ao.OrganizationId;
            return Update<T>(input);
        }

        public Task<T> Update<T>(IOrganizationAccountManager am, OrganizationAccountManagerInput input) where T : OrganizationAccountManagerOutput
        {
            input.OrganizationId = am.OrganizationId;
            return Update<T>(input);
        }
    }
}