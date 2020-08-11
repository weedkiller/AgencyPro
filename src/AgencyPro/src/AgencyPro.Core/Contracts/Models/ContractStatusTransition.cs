// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Contracts.Enums;
using AgencyPro.Core.Data.Infrastructure;

namespace AgencyPro.Core.Contracts.Models
{
    public class ContractStatusTransition : IObjectState
    {
        public int Id { get; set; }
        public Guid ContractId { get; set; }
        public ContractStatus Status { get; set; }
        public ObjectState ObjectState { get; set; }
        public DateTimeOffset Created { get; set; }

    }
}