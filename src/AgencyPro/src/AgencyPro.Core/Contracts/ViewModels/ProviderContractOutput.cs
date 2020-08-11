// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using Newtonsoft.Json;

namespace AgencyPro.Core.Contracts.ViewModels
{
    public abstract class ProviderContractOutput : ContractOutput
    {
        public override Guid TargetOrganizationId => this.ProjectManagerOrganizationId;

        public virtual decimal RecruitingStream => RecruitingAgencyStream + RecruiterStream;
        public virtual decimal MarketingStream => MarketingAgencyStream + MarketerStream;

        [JsonIgnore] public sealed override int RecruitingNumber { get; set; }
        [JsonIgnore] public sealed override int BuyerNumber { get; set; }
        [JsonIgnore] public sealed override int MarketingNumber { get; set; }
        [JsonIgnore] public sealed override string RecruiterName { get; set; }
        [JsonIgnore] public sealed override string RecruiterImageUrl { get; set; }
       
        [JsonIgnore] public sealed override string MarketerName { get; set; }
        [JsonIgnore] public sealed override string MarketerImageUrl { get; set; }

        [JsonIgnore] public sealed override Guid RecruiterId { get; set; }
        [JsonIgnore] public sealed override Guid MarketerId { get; set; }
       
        [JsonIgnore] public sealed override decimal MarketerStream { get; set; }
     
        [JsonIgnore] public sealed override decimal MarketingAgencyStream { get; set; }
      
        [JsonIgnore] public sealed override decimal RecruitingAgencyStream { get; set; }

        [JsonIgnore] public sealed override decimal RecruiterStream { get; set; }
        
        [JsonIgnore] public sealed override decimal MaxRecruiterWeekly { get; set; }

        [JsonIgnore] public sealed override decimal MaxMarketerWeekly { get; set; }
        
        [JsonIgnore] public sealed override decimal MaxRecruitingAgencyWeekly { get; set; }
        [JsonIgnore] public sealed override decimal MaxMarketingAgencyWeekly { get; set; }
    }
}