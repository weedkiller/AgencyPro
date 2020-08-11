// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Runtime.CompilerServices;
using AgencyPro.Core.Exceptions;
using AgencyPro.Core.OrganizationPeople.Services;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.People.Services;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.UserAccount.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace AgencyPro.Middleware.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static IRecruiter GetRecruiter(this IServiceProvider provider)
        {
            var userInfo = provider.GetRequiredService<IUserInfo>();
            var service = provider.GetRequiredService<IRecruiterService>();

            return service.Get(userInfo.UserId).Result;
        }

        public static IOrganizationRecruiter GetOrganizationRecruiter(this IServiceProvider provider)
        {
            var svc = provider.GetRequiredService<IOrganizationRecruiterService>();
            var userInfo = provider.GetRequiredService<IUserInfo>();

            return svc.GetPrincipal(userInfo.UserId, GetOrganizationId(provider)).Result;
        }

        public static IOrganizationContractor GetOrganizationContractor(this IServiceProvider provider)
        {
            var contractorService = provider.GetRequiredService<IOrganizationContractorService>();
            var userInfo = provider.GetRequiredService<IUserInfo>();

            return contractorService.GetPrincipal(userInfo.UserId, GetOrganizationId(provider)).Result;
        }

        public static IPerson GetPerson(this IServiceProvider provider)
        {
            var personSvc = provider.GetRequiredService<IPersonService>();
            var userInfo = provider.GetRequiredService<IUserInfo>();

            return personSvc.Get(userInfo.UserId).Result;
        }

        public static IContractor GetContractor(this IServiceProvider provider)
        {
            var userInfo = provider.GetRequiredService<IUserInfo>();
            var contractorService = provider.GetRequiredService<IContractorService>();

            return contractorService.Get(userInfo.UserId).Result;
        }

        public static IAccountManager GetAccountManager(this IServiceProvider provider)
        {
            var userInfo = provider.GetRequiredService<IUserInfo>();
            var accountManagerService = provider.GetRequiredService<IAccountManagerService>();

            return accountManagerService.Get(userInfo.UserId).Result;
        }

        public static IProjectManager GetProjectManager(this IServiceProvider provider)
        {
            var userInfo = provider.GetRequiredService<IUserInfo>();
            var service = provider.GetRequiredService<IProjectManagerService>();

            return service.Get(userInfo.UserId).Result;
        }

        public static IMarketer GetMarketer(this IServiceProvider provider)
        {
            var userInfo = provider.GetRequiredService<IUserInfo>();
            var service = provider.GetRequiredService<IMarketerService>();

            return service.Get(userInfo.UserId).Result;
        }

        public static ICustomer GetCustomer(this IServiceProvider provider)
        {
            var userInfo = provider.GetRequiredService<IUserInfo>();
            var service = provider.GetRequiredService<ICustomerService>();

            return service.Get(userInfo.UserId).Result;
        }

        public static IOrganizationPerson GetOrganizationPerson(this IServiceProvider provider)
        {
            
            var svc = provider.GetRequiredService<IOrganizationPersonService>();
            var userInfo = provider.GetRequiredService<IUserInfo>();

            return svc.GetPrincipal(userInfo.UserId, GetOrganizationId(provider)).Result;
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(ServiceProviderExtensions)}.{callerName}] - {message}";
        }

        public static IAgencyOwner GetAgencyOwner(this IServiceProvider provider)
        {
            var service = provider.GetRequiredService<IOrganizationCustomerService>();
            var userInfo = provider.GetRequiredService<IUserInfo>();

            return service.GetAgencyOwner(userInfo.UserId, GetOrganizationId(provider)).Result as IAgencyOwner;

        }

        public static IMarketingAgencyOwner GetMarketingAgencyOwner(this IServiceProvider provider)
        {
            var service = provider.GetRequiredService<IOrganizationCustomerService>();
            var userInfo = provider.GetRequiredService<IUserInfo>();

            var result = service.GetAgencyOwner(userInfo.UserId, GetOrganizationId(provider)).Result;
            if (!result.IsMarketingOwner)
                throw new ForbiddenException("User is an agency owner but not does not have marketing features enabled");

            return result as IMarketingAgencyOwner;
        }

        public static IProviderAgencyOwner GetProviderAgencyOwner(this IServiceProvider provider)
        {
            var service = provider.GetRequiredService<IOrganizationCustomerService>();
            var userInfo = provider.GetRequiredService<IUserInfo>();

            var result = service.GetAgencyOwner(userInfo.UserId, GetOrganizationId(provider)).Result;
            if (!result.IsProviderOwner)
                throw new ForbiddenException("User is an agency owner but not does not have provider features enabled");

            return result as IProviderAgencyOwner;
        }

        public static IRecruitingAgencyOwner GetRecruitingAgencyOwner(this IServiceProvider provider)
        {
            var service = provider.GetRequiredService<IOrganizationCustomerService>();
            var userInfo = provider.GetRequiredService<IUserInfo>();

            var result = service.GetAgencyOwner(userInfo.UserId, GetOrganizationId(provider)).Result;
            if (!result.IsRecruitingOwner)
                throw new ForbiddenException("User is an agency owner but not does not have recruiting features enabled");

            return result as IRecruitingAgencyOwner;
        }

        public static IOrganizationAccountManager GetOrganizationAccountManager(this IServiceProvider provider)
        {
            var service = provider.GetRequiredService<IOrganizationAccountManagerService>();
            var userInfo = provider.GetRequiredService<IUserInfo>();

            return service.GetPrincipal(userInfo.UserId, GetOrganizationId(provider)).Result;
        }

        public static IOrganizationCustomer GetOrganizationCustomer(this IServiceProvider provider)
        {
            var svc = provider.GetRequiredService<IOrganizationCustomerService>();
            var userInfo = provider.GetRequiredService<IUserInfo>();

            return svc.GetPrincipal(userInfo.UserId, GetOrganizationId(provider)).Result as IOrganizationCustomer;
        }

        public static IOrganizationMarketer GetOrganizationMarketer(this IServiceProvider provider)
        {
            var svc = provider.GetRequiredService<IOrganizationMarketerService>();
            var userInfo = provider.GetRequiredService<IUserInfo>();

            return svc.GetPrincipal(userInfo.UserId, GetOrganizationId(provider)).Result as IOrganizationMarketer;
        }
        
        private static Guid GetOrganizationId(IServiceProvider provider)
        {
            var request = provider.GetRequiredService<IActionContextAccessor>();

            if (!request.ActionContext.RouteData.Values.ContainsKey("organizationId"))
                throw new ForbiddenException();


            if (!Guid.TryParse(request.ActionContext.RouteData.Values["organizationId"].ToString(), out var id))
                throw new ArgumentException("organization id is not a valid guid");

            return id;
        }
    }
}
