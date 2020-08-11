// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core
{
    /// <summary>
    ///     Indicates that the marked method is assertion method, i.e. it halts control flow if
    ///     one of the conditions is satisfied. To set the condition, mark one of the parameters with
    ///     <see cref="AssertionConditionAttribute" /> attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class AssertionMethodAttribute : Attribute
    {
    }
}