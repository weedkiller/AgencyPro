// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Services;

namespace AgencyPro.Core.UserAccount.Validation
{
    public class AggregateValidator<T> : List<IValidator<T>>, IValidator<T>
        where T : class, IObjectState
    {
        public ValidationResult Validate(IService<T> service, T account, string value)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));
            if (account == null) throw new ArgumentNullException(nameof(account));

            foreach (var item in this)
            {
                var result = item.Validate(service, account, value);
                if (result != null && result != ValidationResult.Success) return result;
            }

            return null;
        }
    }
}