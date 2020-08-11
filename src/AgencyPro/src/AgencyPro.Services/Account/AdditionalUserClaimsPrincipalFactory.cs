// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Constants;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AgencyPro.Core.UserAccount.Models;

namespace AgencyPro.Services.Account
{
    public class AdditionalUserClaimsPrincipalFactory
        : UserClaimsPrincipalFactory<ApplicationUser, Role>
    {
        public AdditionalUserClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            RoleManager<Role> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {

        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var userId = await UserManager.GetUserIdAsync(user);
            var userName = await UserManager.GetUserNameAsync(user);
            var emailConfirmed = user.EmailConfirmed.ToString().ToLower();
            var id = new ClaimsIdentity(IdentityConstants.ApplicationScheme, // REVIEW: Used to match Application scheme
                JwtClaimTypes.Name,
                JwtClaimTypes.Role);

            id.AddClaim(new Claim(JwtClaimTypes.Id, userId));
            id.AddClaim(new Claim(JwtClaimTypes.Subject, userId));
            id.AddClaim(new Claim(JwtClaimTypes.Name, userName));
            id.AddClaim(new Claim(JwtClaimTypes.Email, user.Email));
            id.AddClaim(new Claim(JwtClaimTypes.EmailVerified, emailConfirmed, ClaimValueTypes.Boolean));


            if (UserManager.SupportsUserSecurityStamp)
            {
                id.AddClaim(new Claim("AspNet.Identity.SecurityStamp",
                    await UserManager.GetSecurityStampAsync(user)));
            }

            return id;
        }

        public override async Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);
            var identity = (ClaimsIdentity)principal.Identity;


            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Scope, ScopeNames.PersonScope)
            };

            if (await UserManager.IsInRoleAsync(user, RoleNames.AdminRole))
            {
                claims.Add(new Claim(JwtClaimTypes.Scope, ScopeNames.AdminScope));
                claims.Add(new Claim(JwtClaimTypes.Role, RoleNames.AdminRole));

            }

            if (user.IsAdmin)
            {
                claims.Add(new Claim(JwtClaimTypes.Scope, ScopeNames.AdminScope));
                claims.Add(new Claim(JwtClaimTypes.Role, RoleNames.AdminRole));
            }

            identity.AddClaims(claims);
            return principal;
        }
    }
}