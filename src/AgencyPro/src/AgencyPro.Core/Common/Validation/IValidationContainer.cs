// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;

namespace AgencyPro.Core.Common.Validation
{
    public interface IValidationContainer<out T> : IValidationContainer
    {
        T Entity { get; }
    }

    public interface IValidationContainer
    {
        IDictionary<string, IList<string>> Errors { get; }

        bool IsValid { get; }
    }
}