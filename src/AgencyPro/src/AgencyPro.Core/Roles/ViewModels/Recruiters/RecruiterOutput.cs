// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Roles.Services;

namespace AgencyPro.Core.Roles.ViewModels.Recruiters
{
    public class RecruiterOutput : RecruiterInput, IRecruiter
    {
        public Guid Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }
    }
}