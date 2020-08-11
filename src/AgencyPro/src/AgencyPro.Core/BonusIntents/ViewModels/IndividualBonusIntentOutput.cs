// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Text;
using AgencyPro.Core.BonusIntents.Models;
using AgencyPro.Core.BonusIntents.Services;

namespace AgencyPro.Core.BonusIntents.ViewModels
{
    public class IndividualBonusIntentOutput : IIndividualBonusIntent
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public BonusType BonusType { get; set; }
        public string TransferId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }
    }
}
