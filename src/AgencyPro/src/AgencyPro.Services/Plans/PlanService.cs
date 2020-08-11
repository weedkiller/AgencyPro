// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Plans.Models;
using AgencyPro.Core.Plans.Services;
using AgencyPro.Core.Plans.ViewModels;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Stripe;

using StripePlanService = Stripe.PlanService;

namespace AgencyPro.Services.Plans
{
    public class PlanService : Service<StripePlan>, IPlanService
    {
        private readonly StripePlanService _planService;
        private readonly ILogger<PlanService> _logger;

        public PlanService(
            StripePlanService planService,
            ILogger<PlanService> logger,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _planService = planService;
            _logger = logger;
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(PlanService)}.{callerName}] - {message}";
        }

        public Task<PlanResult> PushPlan(StripePlan plan, bool commit = true)
        {
            var retVal = new PlanResult()
            {
               
            };

            Plan stripePlan = null;
            if (!string.IsNullOrWhiteSpace(plan.StripeId))
                stripePlan = _planService.Get(plan.StripeId);

            if (stripePlan != null)
            {
                stripePlan = _planService.Update(stripePlan.Id, new PlanUpdateOptions
                {
                    Active = plan.IsActive,
                    Nickname = plan.Name,
                    TrialPeriodDays = plan.TrialPeriodDays,
                    Metadata = new Dictionary<string, string>()
                    {
                        {"ref_id", plan.Id.ToString()}
                    }
                });
            }
            else
                stripePlan = _planService.Create(new PlanCreateOptions()
                {
                    Active = plan.IsActive,
                    Amount = Convert.ToInt64(plan.Amount * 100),
                    Currency = "usd",
                    Nickname = plan.Name,
                    Interval = plan.Interval,
                    Product = plan.ProductId,
                    TrialPeriodDays = plan.TrialPeriodDays,
                    Metadata = new Dictionary<string, string>()
                    {
                        {"ref_id", plan.Id.ToString()}
                    }
                });

            plan.StripeId = stripePlan.Id;
            plan.StripeBlob = JsonConvert.SerializeObject(stripePlan);
            plan.ObjectState = ObjectState.Modified;

            var records = Repository.InsertOrUpdateGraph(plan, commit);

            _logger.LogDebug(GetLogMessage("{0} records updated"), records);

            if (records > 0)
            {
                retVal.Succeeded = true;
                retVal.PlanId = plan.StripeId;

            }

            return Task.FromResult(retVal);

        }

        public async Task<int> ExportPlans()
        {
            _logger.LogInformation(GetLogMessage("Exporting Plans..."));

            var internalPlans = Repository.Queryable().ToList();

            var totals = 0;

            foreach (var plan in internalPlans)
            {
                _logger.LogDebug(GetLogMessage("Exporting Plan: {0}"), plan.Id);

                var result = await PushPlan(plan);
                if (result.Succeeded) totals += 1;
            }

            _logger.LogDebug(GetLogMessage("Total Records Updated: {0}"), totals);

            return totals;
        }
    }
}