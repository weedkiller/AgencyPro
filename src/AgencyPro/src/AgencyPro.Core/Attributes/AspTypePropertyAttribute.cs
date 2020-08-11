// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class AspTypePropertyAttribute : Attribute
    {
        public AspTypePropertyAttribute(bool createConstructorReferences)
        {
            CreateConstructorReferences = createConstructorReferences;
        }

        public bool CreateConstructorReferences { get; }
    }
}