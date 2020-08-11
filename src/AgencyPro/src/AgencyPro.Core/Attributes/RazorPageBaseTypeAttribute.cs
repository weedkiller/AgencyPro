// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class RazorPageBaseTypeAttribute : Attribute
    {
        public RazorPageBaseTypeAttribute([NotNull] string baseType)
        {
            BaseType = baseType;
        }

        public RazorPageBaseTypeAttribute([NotNull] string baseType, string pageName)
        {
            BaseType = baseType;
            PageName = pageName;
        }

        [NotNull] public string BaseType { get; }
        [CanBeNull] public string PageName { get; }
    }
}