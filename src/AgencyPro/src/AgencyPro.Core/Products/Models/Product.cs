// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Models;

namespace AgencyPro.Core.Products.Models
{
    public class Product : BaseObjectState
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool Shippable { get; set; }
        public string StatementDescriptor { get; set; }
        public string UnitLabel { get; set; }
        public string StripeId { get; set; }
        public string StripeBlob { get; set; }
        public string UniqueId { get; set; }
        public bool IsActive { get; set; }
        public string Caption { get; set; }
        public string Url { get; set; }
    }
}