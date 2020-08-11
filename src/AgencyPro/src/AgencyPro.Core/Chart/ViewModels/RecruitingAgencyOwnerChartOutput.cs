// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AgencyPro.Core.Chart.ViewModels
{
    public class RecruitingAgencyOwnerChartOutput : ChartOutput<RecruitingAgencyOwnerChartDataItem>
    {
        [JsonIgnore]
        public override Dictionary<string, Dictionary<string, List<RecruitingAgencyOwnerChartDataItem>>> Am { get; set; }

        [JsonIgnore]
        public override Dictionary<string, Dictionary<string, List<RecruitingAgencyOwnerChartDataItem>>> Co { get; set; }

        [JsonIgnore]
        public override Dictionary<string, Dictionary<string, List<RecruitingAgencyOwnerChartDataItem>>> Pm { get; set; }

        [JsonIgnore]
        public override Dictionary<string, Dictionary<string, List<RecruitingAgencyOwnerChartDataItem>>> Ma { get; set; }

        [JsonIgnore]
        public override Dictionary<string, Dictionary<string, List<RecruitingAgencyOwnerChartDataItem>>> Cont
        {
            get;
            set;
        }

        [JsonIgnore]
        public override Dictionary<string, Dictionary<string, List<RecruitingAgencyOwnerChartDataItem>>> Proj
        {
            get;
            set;
        }
        
    }
}