// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Data.Infrastructure;

namespace AgencyPro.Core.Data.Audit
{
    public interface IAuditableEntity : IObjectState
    {
        Guid? CreatedById { get; set; }

        DateTimeOffset Created { get; set; }

        Guid? UpdatedById { get; set; }

        DateTimeOffset Updated { get; set; }
    }
}