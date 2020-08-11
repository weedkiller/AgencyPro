// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

#pragma warning disable 1591
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable IntroduceOptionalParameters.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable InconsistentNaming

namespace AgencyPro.Core
{
    /// <summary>
    ///     Indicates that the value of the marked element could be <c>null</c> sometimes,
    ///     so the check for <c>null</c> is necessary before its usage.
    /// </summary>
    /// <example>
    ///     <code>
    /// [CanBeNull] object Test() => null;
    /// 
    /// void UseTest() {
    ///   var p = Test();
    ///   var s = p.ToString(); // Warning: Possible 'System.NullReferenceException'
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(
        AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property |
        AttributeTargets.Delegate | AttributeTargets.Field | AttributeTargets.Event |
        AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.GenericParameter)]
    public sealed class CanBeNullAttribute : Attribute
    {
    }
}