// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Cards.Services;
using AgencyPro.Core.Config;
using AgencyPro.Core.Stripe.Services;
using AgencyPro.Core.Subscriptions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;
using AgencyPro.Core.BuyerAccounts.Services;
using AgencyPro.Core.Charges.Services;
using AgencyPro.Core.DisperseFunds.Services;
using AgencyPro.Core.FinancialAccounts.Services;
using AgencyPro.Core.Invoices.Services;
using AgencyPro.Core.PaymentIntents.Services;
using AgencyPro.Core.StripeSources.Services;

namespace AgencyPro.Identity.API.Controllers.Webhooks
{


    [Route("webhooks")]
    [AllowAnonymous]
    public partial class WebhooksController : ControllerBase
    {
        private readonly IStripeService _stripeService;
        private readonly IProjectInvoiceService _invoiceService;
        private readonly IDisperseFundsService _paidInvoiceService;
        private readonly IFinancialAccountService _financialAccountService;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly ICardService _cardService;
        private readonly IChargeService _chargeService;
        private readonly ISourceService _sourceService;
        private readonly IPaymentIntentService _paymentIntentService;
        private readonly IBuyerAccountService _buyerAccountService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly ILogger<WebhooksController> _logger;

        public WebhooksController(
            IStripeService stripeService,
            IProjectInvoiceService invoiceService,
            IDisperseFundsService paidInvoiceService,
            IFinancialAccountService financialAccountService,
            ICardService cardService,
            IChargeService chargeService,
            ISourceService sourceService,
            IPaymentIntentService paymentIntentService,
            ISubscriptionService subscriptionService,
            ILogger<WebhooksController> logger,
            IOptions<AppSettings> appSettings, 
            IBuyerAccountService buyerAccountService)
        {
            _logger = logger;
            _cardService = cardService;
            _chargeService = chargeService;
            _sourceService = sourceService;
            _paymentIntentService = paymentIntentService;
            _subscriptionService = subscriptionService;
            _stripeService = stripeService;
            _invoiceService = invoiceService;
            _paidInvoiceService = paidInvoiceService;
            _financialAccountService = financialAccountService;
            _appSettings = appSettings;
            _buyerAccountService = buyerAccountService;
            _logger = logger;
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(WebhooksController)}.{callerName}] - {message}";
        }
    }
}
