// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.PayoutIntents.Services;

namespace AgencyPro.Core.PayoutIntents.ViewModels
{
    public class IndividualPayoutIntentOutput : PayoutViewModel, IIndividualPayoutIntent
    {
        public Guid PersonId { get; set; }
        public string RecipientName { get; set; }
    }
}
