// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AgencyPro.Core.UserAccount.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AgencyPro.Services.Account
{
    public partial class UserAccountManager
    {
        protected override Task<IdentityResult> UpdateUserAsync(ApplicationUser user)
        {
            return UpdateAsync(user, CancellationToken.None);
        }

        public override Task<ApplicationUser> FindByIdAsync(string userId)
        {
            return FindByIdAsync(userId, CancellationToken.None);
        }

        public override Task<ApplicationUser> FindByLoginAsync(string loginProvider, string providerKey)
        {
            return base.FindByLoginAsync(loginProvider, providerKey);
        }

        public override Task<ApplicationUser> FindByNameAsync(string userName)
        {
            return FindByNameAsync(userName, CancellationToken.None);
        }

        public Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var guid = Guid.Parse(userId);
            return Repository.Queryable()
                .Include(x => x.Person)
                .ThenInclude(x => x.OrganizationPeople)
                .ThenInclude(x => x.AccountManager)
                .Include(x => x.Person)
                .ThenInclude(x => x.OrganizationPeople)
                .ThenInclude(x => x.ProjectManager)
                .Include(x => x.Person)
                .ThenInclude(x => x.OrganizationPeople)
                .ThenInclude(x => x.Contractor)
                .Include(x => x.Person)
                .ThenInclude(x => x.OrganizationPeople)
                .ThenInclude(x => x.Customer)
                .Include(x => x.Person)
                .ThenInclude(x => x.OrganizationPeople)
                .ThenInclude(x => x.Recruiter)
                .Include(x => x.Person)
                .ThenInclude(x => x.OrganizationPeople)
                .ThenInclude(x => x.Marketer)
                .Where(x => x.Id == guid)
                .FirstAsync(cancellationToken);
        }

        public Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return Repository.Queryable()
                .Include(x => x.Person)
                .ThenInclude(x => x.OrganizationPeople)
                .ThenInclude(x => x.AccountManager)
                .Include(x => x.Person)
                .ThenInclude(x => x.OrganizationPeople)
                .ThenInclude(x => x.ProjectManager)
                .Include(x => x.Person)
                .ThenInclude(x => x.OrganizationPeople)
                .ThenInclude(x => x.Contractor)
                .Include(x => x.Person)
                .ThenInclude(x => x.OrganizationPeople)
                .ThenInclude(x => x.Customer)
                .Include(x => x.Person)
                .ThenInclude(x => x.OrganizationPeople)
                .ThenInclude(x => x.Recruiter)
                .Include(x => x.Person)
                .ThenInclude(x => x.OrganizationPeople)
                .ThenInclude(x => x.Marketer)
                .Where(x => x.NormalizedUserName == normalizedUserName)
                .FirstAsync(cancellationToken);
        }
    }
}