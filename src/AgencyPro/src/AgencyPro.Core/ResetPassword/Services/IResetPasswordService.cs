// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Threading.Tasks;
using AgencyPro.Core.ResetPassword.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace AgencyPro.Core.ResetPassword.Services
{
    public interface IResetPasswordService
    {
        Task<IdentityResult> ResetPassword(ResetPasswordInputModel model);
    }
}
