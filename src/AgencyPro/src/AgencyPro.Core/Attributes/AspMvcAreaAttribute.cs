// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core
{
    /// <summary>
    ///     ASP.NET MVC attribute. Indicates that a parameter is an MVC area.
    ///     Use this attribute for custom wrappers similar to
    ///     <c>System.Web.Mvc.Html.ChildActionExtensions.RenderAction(HtmlHelper, String)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class AspMvcAreaAttribute : Attribute
    {
        public AspMvcAreaAttribute()
        {
        }

        public AspMvcAreaAttribute([NotNull] string anonymousProperty)
        {
            AnonymousProperty = anonymousProperty;
        }

        [CanBeNull] public string AnonymousProperty { get; }
    }
}