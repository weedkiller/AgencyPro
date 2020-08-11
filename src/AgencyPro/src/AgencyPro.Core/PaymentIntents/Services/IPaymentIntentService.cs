// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Threading.Tasks;
using AgencyPro.Core.PaymentIntents.ViewModels;
using Stripe;

namespace AgencyPro.Core.PaymentIntents.Services
{
    public interface IPaymentIntentService
    {
        Task<PaymentIntentResult> PaymentIntentCreated(PaymentIntent paymentIntent);
        Task<PaymentIntentResult> PaymentIntentUpdated(PaymentIntent paymentIntent);

        Task<PaymentIntentResult> PaymentIntentAmountCapturableUpdated(PaymentIntent paymentIntent);
    }
}
