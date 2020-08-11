// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core
{
    /// <summary>
    ///     For a parameter that is expected to be one of the limited set of values.
    ///     Specify fields of which type should be used as values for this parameter.
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Field,
        AllowMultiple = true)]
    public sealed class ValueProviderAttribute : Attribute
    {
        public ValueProviderAttribute([NotNull] string name)
        {
            Name = name;
        }

        [NotNull] public string Name { get; }
    }
}