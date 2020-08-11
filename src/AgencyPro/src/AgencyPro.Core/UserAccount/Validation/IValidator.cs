// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.ComponentModel.DataAnnotations;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Services;

namespace AgencyPro.Core.UserAccount.Validation
{
    public interface IValidator<T> where T : class, IObjectState
    {
        ValidationResult Validate(IService<T> service, T account, string value);
    }
}