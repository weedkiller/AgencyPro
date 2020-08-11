// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core
{
    /// <summary>
    ///     XAML attribute. Indicates the property of some <c>BindingBase</c>-derived type, that
    ///     is used to bind some item of <c>ItemsControl</c>-derived type. This annotation will
    ///     enable the <c>DataContext</c> type resolve for XAML bindings for such properties.
    /// </summary>
    /// <remarks>
    ///     Property should have the tree ancestor of the <c>ItemsControl</c> type or
    ///     marked with the <see cref="XamlItemsControlAttribute" /> attribute.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class XamlItemBindingOfItemsControlAttribute : Attribute
    {
    }
}