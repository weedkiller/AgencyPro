// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;

namespace AgencyPro.Core.PayoutIntents
{
    public partial class PayoutIntentProjections : Profile
    {
        public PayoutIntentProjections()
        {
            CreateOrganizationPayoutMaps();
            CreateIndividualPayoutMaps();
        }
    }
}
