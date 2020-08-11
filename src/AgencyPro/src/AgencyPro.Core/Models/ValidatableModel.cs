// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgencyPro.Core.Models
{
    public abstract class ValidatableModel : IValidatableObject
    {
        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Override this method to implement custom validation in your entities
            // This is only for making it compile... and returning null will give an exception.
            if (false)
#pragma warning disable 162
                yield return new ValidationResult("Well, this should not happened...");
#pragma warning restore 162
        }
    }
}