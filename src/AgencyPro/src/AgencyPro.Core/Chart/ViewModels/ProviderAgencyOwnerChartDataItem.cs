// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Newtonsoft.Json;

namespace AgencyPro.Core.Chart.ViewModels
{
    public class ProviderAgencyOwnerChartDataItem : ChartDataItem
    {
        [JsonIgnore]
        public override decimal TotalSysStream { get; set; }

        [JsonIgnore]
        public override decimal TotalMaStream { get; set; }

        [JsonIgnore]
        public override decimal TotalReStream { get; set; }

        [JsonIgnore]
        public override decimal TotalRagStream { get; set; }

        [JsonIgnore]
        public override decimal TotalMagStream { get; set; }

        [JsonIgnore]
        public override decimal TotalCuAmount { get; set; }

        public decimal TotalAgencyStream => TotalAgStream;
    }
}