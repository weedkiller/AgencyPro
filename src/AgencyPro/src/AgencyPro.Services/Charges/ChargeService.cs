// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AgencyPro.Core.Charges.Models;
using AgencyPro.Core.Charges.Services;
using AgencyPro.Core.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;
using Stripe;

namespace AgencyPro.Services.Charges
{
    public class ChargeService : Service<StripeCharge>, IChargeService
    {
        private readonly ILogger<ChargeService> _logger;

        public ChargeService(
            ILogger<ChargeService> logger,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _logger = logger;
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(ChargeService)}.{callerName}] - {message}";
        }

        public async Task<int> PullCharge(Charge charge)
        { 
            _logger.LogInformation(GetLogMessage("Charge: {0}, Amount: {1}"), charge.Id, (charge.Amount / 100m).ToString("C"));
            
            var entity = await Repository.Queryable().Where(x => x.Id == charge.Id).FirstOrDefaultAsync();
            if (entity != null)
            {
                entity.ObjectState = ObjectState.Modified;
            }
            else
            {
                entity = new StripeCharge
                {
                    Id = charge.Id,
                    ObjectState = ObjectState.Added
                };
            }


            entity.InjectFrom(charge);

            entity.CustomerId = charge.CustomerId;
            entity.InvoiceId = charge.InvoiceId;
            entity.BalanceTransactionId = charge.BalanceTransactionId;
            entity.OutcomeNetworkStatus = charge.Outcome.NetworkStatus;
            entity.OutcomeReason = charge.Outcome.Reason;
            entity.OutcomeRiskLevel = charge.Outcome.RiskLevel;
            entity.OutcomeRiskScore = charge.Outcome.RiskScore;
            entity.OutcomeSellerMessage = charge.Outcome.SellerMessage;
            entity.OutcomeType = charge.Outcome.Type;
            entity.Captured = charge.Captured;
            entity.ReceiptEmail = charge.ReceiptEmail;
            entity.ReceiptNumber = charge.ReceiptNumber;
            entity.Amount = charge.Amount / 100m;
            entity.Disputed = charge.Disputed;


            var records =  Repository.InsertOrUpdateGraph(entity, true);
            _logger.LogDebug(GetLogMessage("{0} records updated"), records);



            return records;
        }
    }
}
