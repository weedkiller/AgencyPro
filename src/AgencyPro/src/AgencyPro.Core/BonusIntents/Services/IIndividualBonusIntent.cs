// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.BonusIntents.Models;

namespace AgencyPro.Core.BonusIntents.Services
{
    public interface IIndividualBonusIntent
    {
        Guid Id { get; set; }
        Guid PersonId { get; set; }
        BonusType BonusType { get; set; }
        string TransferId { get; set; }
        decimal Amount { get; set; }
        string Description { get; set; }
        DateTimeOffset Created { get; set; }
        DateTimeOffset Updated { get; set; }
    }
}