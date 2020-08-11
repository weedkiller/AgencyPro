// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AgencyPro.Core.Chart.ViewModels
{
    public class ProjectManagerChartOutput : ChartOutput<ProjectManagerChartDataItem>
    {
        [JsonIgnore] public override Dictionary<string, Dictionary<string, List<ProjectManagerChartDataItem>>> Re { get; set; }
        [JsonIgnore] public override Dictionary<string, Dictionary<string, List<ProjectManagerChartDataItem>>> Pm { get; set; }
        [JsonIgnore] public override Dictionary<string, Dictionary<string, List<ProjectManagerChartDataItem>>> Ma { get; set; }

    }
}
