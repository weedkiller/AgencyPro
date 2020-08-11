// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AgencyPro.Core.Charges.Models;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.PaymentIntents.Models;
using AgencyPro.Core.PaymentIntents.Services;
using AgencyPro.Core.PaymentIntents.ViewModels;
using AgencyPro.Core.PayoutIntents.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Stripe;

namespace AgencyPro.Services.OrganizationPayoutIntents
{
    public class PaymentIntentService : Service<OrganizationPayoutIntent>, IPaymentIntentService
    {
        private readonly ILogger<PaymentIntentService> _logger;
        private readonly IRepositoryAsync<StripeCharge> _charges;
        private readonly IRepositoryAsync<StripePaymentIntent> _paymentIntents;

        public PaymentIntentService(
            ILogger<PaymentIntentService> logger, 
            IServiceProvider serviceProvider) :
            base(serviceProvider)
        {
            _logger = logger;

            _charges = UnitOfWork.RepositoryAsync<StripeCharge>();
            _paymentIntents = UnitOfWork.RepositoryAsync<StripePaymentIntent>();
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(PaymentIntentService)}.{callerName}] - {message}";
        }

        
       

        public Task<PaymentIntentResult> PaymentIntentAmountCapturableUpdated(PaymentIntent paymentIntent)
        {
            return PaymentIntentUpdated(paymentIntent);
        }


        public async Task<PaymentIntentResult> PaymentIntentCreated(PaymentIntent paymentIntent)
        {
            _logger.LogInformation(GetLogMessage("Payment Intent: {0}"), paymentIntent.Id);
            var retVal = new PaymentIntentResult()
            {
                PaymentIntentId = paymentIntent.Id
            };
            
            

            var pi = new StripePaymentIntent
            {
                Id = paymentIntent.Id,
                ObjectState = ObjectState.Added,
                CustomerId = paymentIntent.CustomerId,
                Amount = paymentIntent.Amount.GetValueOrDefault() / 100m,
                AmountCapturable = paymentIntent.AmountCapturable.GetValueOrDefault() / 100m,
                Created = paymentIntent.Created,
                Description = paymentIntent.Description,
                InvoiceId = paymentIntent.InvoiceId,
                AmountReceived = paymentIntent.AmountReceived.GetValueOrDefault() / 100m,
                TransferGroup = paymentIntent.TransferGroup,

                CancelledAt = paymentIntent.CanceledAt,
                Updated = DateTimeOffset.UtcNow,
                CaptureMethod = paymentIntent.CaptureMethod,
                ConfirmationMethod = paymentIntent.ConfirmationMethod
            };
            
            var records = _paymentIntents.InsertOrUpdateGraph(pi, true);

            _logger.LogDebug(GetLogMessage("{0} Payment Intent Records Updated"), records);

            if (records > 0)
            {
                retVal.Succeeded = true;
            }

            return await Task.FromResult(retVal);
        }

        public async Task<PaymentIntentResult> PaymentIntentUpdated(PaymentIntent paymentIntent)
        {
            _logger.LogInformation(GetLogMessage("Payment Intent: {0}"), paymentIntent.Id);

            var retVal = new PaymentIntentResult()
            {
                PaymentIntentId = paymentIntent.Id
            };

            var entity = await _paymentIntents.Queryable().Where(x => x.Id == paymentIntent.Id)
                .FirstOrDefaultAsync();

            if (entity != null)
            {
                entity.Amount =paymentIntent.Amount ?? paymentIntent.Amount.GetValueOrDefault() / 100m;
                entity.AmountCapturable = paymentIntent.AmountCapturable ?? paymentIntent.AmountCapturable.GetValueOrDefault() / 100m;
                entity.AmountReceived = paymentIntent.AmountReceived ?? paymentIntent.AmountReceived.GetValueOrDefault() / 100m;
                entity.CancelledAt = paymentIntent.CanceledAt;
                entity.ConfirmationMethod = paymentIntent.ConfirmationMethod;
                entity.CaptureMethod = paymentIntent.CaptureMethod;
                entity.Description = paymentIntent.Description;
                entity.CustomerId = paymentIntent.CustomerId;
                entity.InvoiceId = paymentIntent.InvoiceId;
                entity.TransferGroup = paymentIntent.TransferGroup;
            }

            var results = _paymentIntents.InsertOrUpdateGraph(entity, true);

            _logger.LogDebug(GetLogMessage("{0} updated payment intent records"), results);
            if (results > 0)
            {
                retVal.Succeeded = true;
            }

            return retVal;

        }
        
    }
}
