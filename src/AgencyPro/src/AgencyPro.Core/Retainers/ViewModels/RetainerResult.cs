// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.ViewModels;

namespace AgencyPro.Core.Retainers.ViewModels
{
    public class RetainerResult : BaseResult
    {
       public Guid? RetainerId { get; set; }
       public decimal? CurrentBalance { get; set; }
       public decimal? TopOffAmount { get; set; }
    }
}
