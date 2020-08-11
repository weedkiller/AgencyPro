// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.Roles.ViewModels.Marketers
{
    public class MarketerInput : MarketerUpdateInput
    {
        public virtual string AffiliateId { get; set; }

        public virtual Guid PersonId { get; set; }
    }
}