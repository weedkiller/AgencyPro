// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.Subscriptions.ViewModels;

namespace AgencyPro.Core.Subscriptions
{
    public partial class SubscriptionProjections :Profile
    {
        public SubscriptionProjections()
        {
            CreateMap<StripeSubscription, SubscriptionOutput>()
                .IncludeAllDerived();

            CreateMap<OrganizationSubscription, SubscriptionOutput>()
                .IncludeMembers(x=>x.StripeSubscription)
                .IncludeAllDerived();

            CreateEmailMappings();
        }
    }
}
