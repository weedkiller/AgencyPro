// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using AgencyPro.Core.Data.Infrastructure;

namespace AgencyPro.Core.Models
{
    public abstract class BaseObjectState : ValidatableModel, IObjectState
    {
        [NotMapped] [IgnoreDataMember] public ObjectState ObjectState { get; set; }
    }
}