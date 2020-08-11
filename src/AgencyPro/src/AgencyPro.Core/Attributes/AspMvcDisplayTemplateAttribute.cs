// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core
{
    /// <summary>
    ///     ASP.NET MVC attribute. Indicates that a parameter is an MVC display template.
    ///     Use this attribute for custom wrappers similar to
    ///     <c>System.Web.Mvc.Html.DisplayExtensions.DisplayForModel(HtmlHelper, String)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class AspMvcDisplayTemplateAttribute : Attribute
    {
    }
}