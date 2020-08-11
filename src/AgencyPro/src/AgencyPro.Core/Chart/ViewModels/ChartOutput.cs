// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;

namespace AgencyPro.Core.Chart.ViewModels
{
    public class ChartOutput<T> where T : ChartDataItem
    {
        public virtual Dictionary<string, string> DateRanges { get; set; }
        public virtual Dictionary<string, string> Breakdowns { get; set; }
        public virtual string CurrentDateRange { get; set; }
        public virtual string CurrentBreakdown { get; set; }
        public virtual Dictionary<string, Dictionary<string, List<T>>> Status { get; set; }
        public virtual Dictionary<string, Dictionary<string, List<T>>> Am { get; set; }
        public virtual Dictionary<string, Dictionary<string, List<T>>> Proj { get; set; }
        public virtual Dictionary<string, Dictionary<string, List<T>>> Cont { get; set; }
        public virtual Dictionary<string, Dictionary<string, List<T>>> Re { get; set; }
        public virtual Dictionary<string, Dictionary<string, List<T>>> Co { get; set; }
        public virtual Dictionary<string, Dictionary<string, List<T>>> Pm { get; set; }
        public virtual Dictionary<string, Dictionary<string, List<T>>> Ma { get; set; }
    }
}
