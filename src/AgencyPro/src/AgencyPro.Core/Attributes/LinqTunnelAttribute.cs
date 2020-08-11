// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core
{
    /// <summary>
    ///     Indicates that method is pure LINQ method, with postponed enumeration (like Enumerable.Select,
    ///     .Where). This annotation allows inference of [InstantHandle] annotation for parameters
    ///     of delegate type by analyzing LINQ method chains.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class LinqTunnelAttribute : Attribute
    {
    }
}