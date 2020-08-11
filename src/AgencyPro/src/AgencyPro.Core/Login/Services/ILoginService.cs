// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Threading.Tasks;
using AgencyPro.Core.Login.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace AgencyPro.Core.Login.Services
{
    public interface ILoginService
    {
        Task<SignInResult> Login(LoginInputModel input);
    }
}
