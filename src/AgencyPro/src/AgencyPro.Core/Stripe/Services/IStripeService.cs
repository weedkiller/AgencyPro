// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.People.Services;
using System.Threading.Tasks;

namespace AgencyPro.Core.Stripe.Services
{
    public interface IStripeService
    {

        
        Task<string> GetAuthUrl(IAgencyOwner customer);

        Task<string> GetAuthUrl(IPerson person);

        Task<string> GetStripeUrl(IPerson person);
        Task<string> GetStripeUrl(IAgencyOwner agencyOwner, bool isRecursive = false);
        
    }
}