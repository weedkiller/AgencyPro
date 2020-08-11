// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Threading.Tasks;
using AgencyPro.Core.Plans.Models;
using AgencyPro.Core.Plans.ViewModels;

namespace AgencyPro.Core.Plans.Services
{
    public interface IPlanService
    {
        Task<PlanResult> PushPlan(StripePlan plan, bool commit = true);


        Task<int> ExportPlans();

    }
}