// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.PayoutIntents.Services;

namespace AgencyPro.Core.PayoutIntents.ViewModels
{

    public class OrganizationPayoutIntentOutput : PayoutViewModel, IOrganizationPayoutIntent
    {
        public string OrganizationName { get; set; }
    }
}