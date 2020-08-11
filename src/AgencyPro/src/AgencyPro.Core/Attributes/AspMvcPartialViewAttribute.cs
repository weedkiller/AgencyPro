// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core
{
    /// <summary>
    ///     ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter is an MVC
    ///     partial view. If applied to a method, the MVC partial view name is calculated implicitly
    ///     from the context. Use this attribute for custom wrappers similar to
    ///     <c>System.Web.Mvc.Html.RenderPartialExtensions.RenderPartial(HtmlHelper, String)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
    public sealed class AspMvcPartialViewAttribute : Attribute
    {
    }
}