// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;

namespace AgencyPro.Core.Common.Validation
{
    #region

    #endregion

    public class ValidationContainer<T> : IValidationContainer<T>
    {
        public ValidationContainer(IDictionary<string, IList<string>> errors, T entity)
        {
            Errors = errors;
            Entity = entity;
        }

        public IDictionary<string, IList<string>> Errors { get; }
        public T Entity { get; }

        public bool IsValid => Errors.Count == 0;
    }
}