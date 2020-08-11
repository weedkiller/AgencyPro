// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Categories.Models;
using AgencyPro.Core.Models;

namespace AgencyPro.Core.PaymentTerms.Models
{
    public class CategoryPaymentTerm : BaseObjectState
    {
        public int CategoryId { get; set; }
        public int PaymentTermId { get; set; }
        public Category Category { get; set; }
        public PaymentTerm PaymentTerm { get; set; }
    }
}