// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Transfers.Services;
using Newtonsoft.Json;

namespace AgencyPro.Core.Transfers.ViewModels
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class TransferOutput : IStripeTransfer
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountReversed { get; set; }
        public string Description { get; set; }
        public string DestinationId { get; set; }
        public string DestinationPaymentId { get; set; }

        public string InvoiceId { get; set; }

        public bool IndividualTransfer { get; set; }
        public bool OrganizationTransfer => !IndividualTransfer;
        public string RecipientName { get; set; }
        public string OrganizationName { get; set; }
        public DateTimeOffset Created { get; set; }
    }
}
