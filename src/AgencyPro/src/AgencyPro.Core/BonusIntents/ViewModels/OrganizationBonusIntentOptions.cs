// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.BonusIntents.ViewModels
{
    public class CreateBonusIntentOptions
    {
        public Guid? LeadId { get; set; }
        public Guid? CandidateId { get; set; }
    }
}