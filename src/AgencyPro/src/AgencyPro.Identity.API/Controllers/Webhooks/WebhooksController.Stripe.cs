// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stripe;
using System.IO;
using System.Threading.Tasks;

namespace AgencyPro.Identity.API.Controllers.Webhooks
{
    public partial class WebhooksController
    {
        [HttpPost("stripe")]
        public async Task<IActionResult> Stripe()
        {
            try
            {
                var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

                //_logger.LogDebug(GetLogMessage(json));

                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], 
                    _appSettings.Value.Stripe.WebhookSigningKey, 
                    throwOnApiVersionMismatch:false);

                _logger.LogInformation(GetLogMessage("Event Id: {0}"), stripeEvent.Id);
                _logger.LogInformation(GetLogMessage("Event Type: {Event}"), stripeEvent.Type);
                //_logger.LogDebug(GetLogMessage("{@event}"), stripeEvent);


                switch (stripeEvent.Type)
                {
                    case Events.SourceChargeable:
                        {
                            if (stripeEvent.Data.Object is Source source)
                            {
                                /* note: you can't get the customer info from here, so source needs
                                 to be created and assigned before this is useful.  This is for source types that
                                 are approved asynchronously.
                                */
                                var result = await _sourceService.SourceChargeable(source);
                                _logger.LogDebug(GetLogMessage("Source Chargeable: {@result}"), result);
                            }

                            break;
                        }
                    case Events.PaymentMethodAttached:
                        {
                            if (stripeEvent.Data.Object is Card card)
                            {
                                await _cardService.PullCard(card);
                            }

                            break;
                        }

                    case Events.IssuingCardCreated:
                    case Events.IssuingCardUpdated:
                        {
                            var card = stripeEvent.Data.Object as Card;
                            await _cardService.PullCard(card);
                            break;
                        }

                    case Events.ChargeCaptured:
                    case Events.ChargeExpired:
                    case Events.ChargeRefunded:
                    case Events.ChargeFailed:
                    case Events.ChargeSucceeded:
                        {
                            var charge = stripeEvent.Data.Object as Charge;
                            await _chargeService.PullCharge(charge);
                            break;
                        }

                    case Events.CustomerDeleted:
                    case Events.CustomerCreated:
                    case Events.CustomerUpdated:
                        {
                            var customer = stripeEvent.Data.Object as Customer;
                            await _buyerAccountService.PullCustomer(customer);
                            break;
                        }

                    case Events.AccountExternalAccountCreated:
                    case Events.AccountExternalAccountUpdated:
                    case Events.AccountExternalAccountDeleted:
                        {
                            var card = stripeEvent.Data.Object as IExternalAccount;
                            await _cardService.PullCard(card);
                            break;
                        }
                    case Events.AccountUpdated:
                        {
                            var account = stripeEvent.Data.Object as Stripe.Account;
                            await _financialAccountService.AccountCreatedOrUpdated(account);
                            break;
                        }
                    case Events.CustomerSubscriptionCreated:
                    case Events.CustomerSubscriptionUpdated:
                    case Events.CustomerSubscriptionTrialWillEnd:
                    case Events.CustomerSubscriptionDeleted:
                        {
                            var sub = stripeEvent.Data.Object as Subscription;
                            await _subscriptionService.PullSubscription(sub);
                            break;
                        }
                    case Events.InvoiceItemUpdated:
                        {
                            if (stripeEvent.Data.Object is InvoiceItem invoiceItem)
                            {
                                var result = await _invoiceService.InvoiceItemUpdated(invoiceItem);

                                _logger.LogInformation(GetLogMessage("Invoice Item Updated: {@result}"), result);
                            }

                            break;
                        }

                    case Events.InvoiceItemDeleted:
                        {

                            if (stripeEvent.Data.Object is InvoiceItem invoiceItem)
                            {
                                var result = await _invoiceService.InvoiceItemDeleted(invoiceItem);

                                _logger.LogInformation(GetLogMessage("Invoice Item Deleted: {@result}"), result);

                            }

                            break;
                        }

                    case Events.InvoiceSent:
                        {
                            if (stripeEvent.Data.Object is Invoice invoice)
                            {
                                var result = await _invoiceService.InvoiceSent(invoice);

                                _logger.LogInformation(GetLogMessage("Invoice Sent: {@result}"), result);
                            }

                            break;
                        }
                    case Events.InvoiceUpdated:
                        {
                            if (stripeEvent.Data.Object is Invoice invoice)
                            {
                                var result = await _invoiceService.InvoiceUpdated(invoice);

                                _logger.LogInformation(GetLogMessage("Invoice Updated: {@result}"), result);
                            }

                            break;
                        }
                   
                    case Events.InvoiceDeleted:
                        {
                            if (stripeEvent.Data.Object is Invoice invoice)
                            {
                                var result = await _invoiceService.InvoiceDeleted(invoice);

                                _logger.LogInformation(GetLogMessage("Invoice Deleted: {@result}"), result);
                            }

                            break;
                        }

                    case Events.InvoiceFinalized:
                        {
                            if (stripeEvent.Data.Object is Invoice invoice)
                            {
                                var result = await _invoiceService.InvoiceFinalized(invoice);
                                _logger.LogInformation(GetLogMessage("Invoice Finalized: {@result}"), result);
                            }

                            break;
                        }
                    case Events.InvoicePaymentSucceeded:
                        {
                            if (stripeEvent.Data.Object is Invoice invoice)
                            {
                                var result = await _invoiceService.InvoicePaymentSucceeded(invoice);

                                _logger.LogDebug(GetLogMessage("Invoice Payment Succeeded: {@result}"), result);
                            }

                            break;
                        }
                    case Events.PaymentIntentCreated:
                        {
                            
                            if (stripeEvent.Data.Object is PaymentIntent paymentIntent)
                            {
                                var result = await _paymentIntentService.PaymentIntentCreated(paymentIntent);

                                _logger.LogDebug(GetLogMessage("Payment Intent Created: {@result}"), result);

                            }


                            break;


                        }
                    case Events.PaymentIntentSucceeded:
                        {
                            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                            await _paymentIntentService.PaymentIntentUpdated(paymentIntent);
                            break;
                        }
                    case Events.CheckoutSessionCompleted:
                        {
                            _logger.LogDebug(GetLogMessage("Checkout session completed"));
                            break;
                        }
                }

                return Ok();
            }
            catch (StripeException ex)
            {
                _logger.LogError(ex, ex.Message);
                return NoContent();
            }
        }
    }
}