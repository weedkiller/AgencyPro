// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using AgencyPro.Core.BillingCategories.ViewModels;
using AgencyPro.Core.PaymentTerms.ViewModels;
using AgencyPro.Core.Positions.ViewModels;
using AgencyPro.Core.Skills.ViewModels;

namespace AgencyPro.Core.Organizations.ProviderOrganizations.ViewModels
{
    public abstract class ProviderOrganizationDetailsOutput : ProviderOrganizationOutput
    {
        public virtual ICollection<PositionOutput> AvailablePositions { get; set; }
        public virtual ICollection<BillingCategoryOutput> AvailableBillingCategories { get; set; }
        public virtual ICollection<PaymentTermOutput> AvailablePaymentTerms { get; set; }
        public virtual ICollection<SkillOutput> AvailableSkills { get; set; }

        public virtual IDictionary<Guid, int> Skills { get; set; }

        public virtual IDictionary<int, bool> PaymentTerms { get; set; }
        public virtual IList<int> BillingCategories { get; set; }
        public virtual IList<int> Positions { get; set; }
    }
}