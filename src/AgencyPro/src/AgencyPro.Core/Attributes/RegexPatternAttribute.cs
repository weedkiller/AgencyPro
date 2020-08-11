// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core
{
    /// <summary>
    ///     Indicates that parameter is regular expression pattern.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class RegexPatternAttribute : Attribute
    {
    }
}