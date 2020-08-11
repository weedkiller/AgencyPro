// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.Roles.Services
{
    public interface IProjectManager
    {
        Guid Id { get; set; }
        DateTimeOffset Created { get; set; }
        DateTimeOffset Updated { get; set; }
    }
}