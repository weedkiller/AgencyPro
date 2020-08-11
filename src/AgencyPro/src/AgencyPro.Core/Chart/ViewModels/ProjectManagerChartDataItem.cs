﻿// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Newtonsoft.Json;

namespace AgencyPro.Core.Chart.ViewModels
{
    public class ProjectManagerChartDataItem : ChartDataItem
    {
        [JsonIgnore]
        public override decimal TotalAgStream { get; set; }

        [JsonIgnore]
        public override decimal TotalRagStream { get; set; }

        [JsonIgnore]
        public override decimal TotalMagStream { get; set; }

        [JsonIgnore]
        public override decimal TotalMaStream { get; set; }

        [JsonIgnore]
        public override decimal TotalReStream { get; set; }

        public override decimal TotalPmStream { get; set; }

        [JsonIgnore]
        public override decimal TotalAmStream { get; set; }

        [JsonIgnore]
        public override decimal TotalCoStream { get; set; }

        [JsonIgnore]
        public override decimal TotalCuAmount { get; set; }

        [JsonIgnore]
        public override decimal TotalSysStream { get; set; }
    }
}