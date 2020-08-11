// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.Subscriptions.Emails;

namespace AgencyPro.Core.Subscriptions
{
    public partial class SubscriptionProjections
    {
        private void CreateEmailMappings()
        {
            CreateMap<StripeSubscription, CustomerSubscriptionEmail>();

            CreateMap<OrganizationSubscription, CustomerSubscriptionEmail>()
                .IncludeMembers(x=>x.StripeSubscription);
        }
    }
}