// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.PaymentIntents.Models;
using AgencyPro.Core.PaymentIntents.ViewModels;

namespace AgencyPro.Core.PaymentIntents
{
    public class PaymentIntentProjections : Profile
    {
        public PaymentIntentProjections()
        {
            CreateMap<StripePaymentIntent, PaymentOutput>();
        }
    }
}
