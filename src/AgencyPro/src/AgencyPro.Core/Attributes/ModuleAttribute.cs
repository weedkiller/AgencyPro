// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Enums;

namespace AgencyPro.Core
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ModuleAttribute : Attribute
    {
        public ModelType Name { get; set; }
        public bool LiveUpdate { get; set; }
    }
}