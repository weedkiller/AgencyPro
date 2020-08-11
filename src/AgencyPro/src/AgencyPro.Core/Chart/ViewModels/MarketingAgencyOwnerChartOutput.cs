// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AgencyPro.Core.Chart.ViewModels
{
    public class MarketingAgencyOwnerChartOutput : ChartOutput<MarketingAgencyOwnerChartDataItem>
    {
        [JsonIgnore]
        public override Dictionary<string, Dictionary<string, List<MarketingAgencyOwnerChartDataItem>>> Am { get; set; }

        [JsonIgnore]
        public override Dictionary<string, Dictionary<string, List<MarketingAgencyOwnerChartDataItem>>> Co { get; set; }

        [JsonIgnore]
        public override Dictionary<string, Dictionary<string, List<MarketingAgencyOwnerChartDataItem>>> Pm { get; set; }

        [JsonIgnore]
        public override Dictionary<string, Dictionary<string, List<MarketingAgencyOwnerChartDataItem>>> Re { get; set; }

        [JsonIgnore]
        public override Dictionary<string, Dictionary<string, List<MarketingAgencyOwnerChartDataItem>>> Cont
        {
            get;
            set;
        }

        [JsonIgnore]
        public override Dictionary<string, Dictionary<string, List<MarketingAgencyOwnerChartDataItem>>> Proj
        {
            get;
            set;
        }
    }
}