﻿// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Threading.Tasks;
using AgencyPro.Core.Registration.ViewModels;

namespace AgencyPro.Core.Registration.Services
{
    public interface IRegistrationService
    {
        Task<RegistrationResult> Register(RegisterInputModel model);
    }
}
