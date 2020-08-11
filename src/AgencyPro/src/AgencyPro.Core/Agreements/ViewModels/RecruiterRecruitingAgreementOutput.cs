// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Newtonsoft.Json;

namespace AgencyPro.Core.Agreements.ViewModels
{
    public class RecruiterRecruitingAgreementOutput : RecruitingAgreementOutput
    {
        [JsonIgnore]
        public override decimal RecruitingAgencyBonus { get; set; }

        [JsonIgnore]
        public override decimal RecruitingAgencyStream { get; set; }

        [JsonIgnore]
        public override decimal RecruitingBonus { get; }

        [JsonIgnore]
        public override decimal RecruitingStream { get; }

        [JsonIgnore]
        public override bool InitiatedByProvider { get; set; }
    }
}