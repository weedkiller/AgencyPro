// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AgencyPro.Core.Chart.ViewModels
{
    public class RecruiterChartOutput : ChartOutput<RecruiterChartDataItem>
    {
        [JsonIgnore] public override Dictionary<string, Dictionary<string, List<RecruiterChartDataItem>>> Proj { get; set; }
        [JsonIgnore] public override Dictionary<string, Dictionary<string, List<RecruiterChartDataItem>>> Re { get; set; }
        [JsonIgnore] public override Dictionary<string, Dictionary<string, List<RecruiterChartDataItem>>> Pm { get; set; }
        [JsonIgnore] public override Dictionary<string, Dictionary<string, List<RecruiterChartDataItem>>> Ma { get; set; }

    }
}
