// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.BonusIntents;
using AgencyPro.Core.BonusIntents.Models;
using AgencyPro.Core.Models;
using AgencyPro.Core.Transfers.Models;

namespace AgencyPro.Core.PayoutIntents.Models
{
    public class BonusTransfer : BaseObjectState
    {
        public string TransferId { get; set; }
        public StripeTransfer Transfer { get; set; }

        public ICollection<IndividualBonusIntent> IndividualBonusIntents { get; set; }
        public ICollection<OrganizationBonusIntent> OrganizationBonusIntents { get; set; }
    }
}