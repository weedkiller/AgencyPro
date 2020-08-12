// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Config;
using AgencyPro.Core.Constants;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace AgencyPro.Middleware
{
    public class Config
    {
        // scopes define the API resources in your system
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                // product scopes
                new ApiResource(ScopeNames.AccountManagerScope, "Account Manager API")
                {
                    ApiSecrets = new List<Secret>(){
                        new Secret("password".Sha256())
                    },
                    UserClaims = { JwtClaimTypes.Scope }
                },
                new ApiResource(ScopeNames.AgencyOwnerScope, "Agency Owner API")
                {
                    ApiSecrets = new List<Secret>(){
                        new Secret("password".Sha256())
                    },
                    UserClaims = { JwtClaimTypes.Scope}
                },
                new ApiResource(ScopeNames.ProjectManagerScope, "Project Manager API")
                {
                    ApiSecrets = new List<Secret>(){
                        new Secret("password".Sha256())
                    },
                    UserClaims = { JwtClaimTypes.Scope}
                },
                new ApiResource(ScopeNames.ContractorScope, "Contractor API")
                {
                    ApiSecrets = new List<Secret>(){
                        new Secret("password".Sha256())
                    },
                    UserClaims = { JwtClaimTypes.Scope}
                },
                new ApiResource(ScopeNames.PersonScope, "Person API")
                {
                    ApiSecrets = new List<Secret>(){
                        new Secret("password".Sha256())
                    },
                    UserClaims = { JwtClaimTypes.Scope}
                },
                new ApiResource(ScopeNames.MarketerScope, "Marketer API")
                {
                    ApiSecrets = new List<Secret>(){
                        new Secret("password".Sha256())
                    },
                    UserClaims = { JwtClaimTypes.Scope}
                },
                new ApiResource(ScopeNames.RecruiterScope, "Recruiter API")
                {
                    ApiSecrets = new List<Secret>(){
                        new Secret("password".Sha256())
                    },
                    UserClaims = { JwtClaimTypes.Scope}
                },
                new ApiResource(ScopeNames.CustomerScope, "Customer API")
                {
                    ApiSecrets = new List<Secret>(){
                        new Secret("password".Sha256())
                    },
                    UserClaims = { JwtClaimTypes.Scope}
                },
                 new ApiResource(ScopeNames.AgencyScope, "Agency API")
                {
                    ApiSecrets = new List<Secret>(){
                        new Secret("password".Sha256())
                    },
                    UserClaims = { JwtClaimTypes.Scope}
                },
                 new ApiResource(ScopeNames.AdminScope, "Admin API")
                 {
                     ApiSecrets = new List<Secret>(){
                         new Secret("password".Sha256())
                     },
                     UserClaims = { JwtClaimTypes.Scope}
                 }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients(AppSettings settings)
        {

            // client credentials client
            return new List<Client>
            {
                 new Client
                {
                    ClientId = "portalAngularClient",
                    ClientSecrets =
                    {
                        new Secret("portalAngularClient".Sha256())
                    },

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 7200, IdentityTokenLifetime = 7200,
                    AllowRememberConsent = true,
                    RequireConsent = false,

                    AllowedCorsOrigins = { settings.Urls.Portal },
                    RedirectUris = { settings.Urls.PortalRedirect },
                    PostLogoutRedirectUris = { settings.Urls.PortalPostLogoutRedirect },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Phone,
                        ScopeNames.PersonScope,
                        ScopeNames.CustomerScope,
                        ScopeNames.ContractorScope,
                        ScopeNames.RecruiterScope,
                        ScopeNames.MarketerScope,
                        ScopeNames.AccountManagerScope,
                        ScopeNames.ProjectManagerScope,
                        ScopeNames.AgencyScope,
                        ScopeNames.AgencyOwnerScope,
                        ScopeNames.AdminScope
                    }
                },
                 new Client
                 {
                     ClientId = "admin",
                     ClientSecrets =
                     {
                         new Secret("adminClient".Sha256())
                     },

                     AllowedGrantTypes = GrantTypes.Implicit,
                     AllowAccessTokensViaBrowser = true,
                     AccessTokenLifetime = 7200, IdentityTokenLifetime = 7200,
                     AllowRememberConsent = true,
                     RequireConsent = false,

                     AllowedCorsOrigins = { settings.Urls.Admin },
                     RedirectUris = { settings.Urls.AdminRedirect },
                     PostLogoutRedirectUris = { settings.Urls.AdminPostLogoutRedirect },
                     AllowedScopes = new List<string>
                     {
                         IdentityServerConstants.StandardScopes.OpenId,
                         IdentityServerConstants.StandardScopes.Profile,
                         IdentityServerConstants.StandardScopes.Email,
                         IdentityServerConstants.StandardScopes.Phone,
                         ScopeNames.PersonScope,
                         ScopeNames.AdminScope,
                     }
                 },
                 new Client
                {
                    ClientId = "angularClient",
                    ClientSecrets =
                    {
                        new Secret("angularClient".Sha256())
                    },

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 7200, IdentityTokenLifetime = 7200,
                    AllowRememberConsent = true,
                    RequireConsent = false,

                    AllowedCorsOrigins = { settings.Urls.Flow },
                    RedirectUris = { settings.Urls.FlowRedirect },
                    PostLogoutRedirectUris = { settings.Urls.FlowPostLogoutRedirect},
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Phone,
                        ScopeNames.PersonScope,
                        ScopeNames.CustomerScope,
                        ScopeNames.ContractorScope,
                        ScopeNames.RecruiterScope,
                        ScopeNames.MarketerScope,
                        ScopeNames.AccountManagerScope,
                        ScopeNames.ProjectManagerScope,
                        ScopeNames.AgencyScope,
                        ScopeNames.AgencyOwnerScope,
                        ScopeNames.AdminScope
                    }
                },
                new Client // currently in use
                {
                    ClientId = "postman2",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientClaimsPrefix = "",
                    AccessTokenLifetime = 400000,
                    ClientSecrets =
                    {
                        new Secret("trinidad".Sha256())
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Phone,
                        ScopeNames.PersonScope,
                        ScopeNames.CustomerScope,
                        ScopeNames.ContractorScope,
                        ScopeNames.RecruiterScope,
                        ScopeNames.MarketerScope,
                        ScopeNames.AccountManagerScope,
                        ScopeNames.ProjectManagerScope,
                        ScopeNames.AgencyScope,
                        ScopeNames.AgencyOwnerScope,
                        ScopeNames.AdminScope
                    }
                }
            };
        }
    }
}