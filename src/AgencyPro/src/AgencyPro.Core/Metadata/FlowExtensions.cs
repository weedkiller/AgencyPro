// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;

namespace AgencyPro.Core.Metadata
{
    public static class FlowExtensions
    {
        public static string GetPath(Type t)
        {
            if (t.GetCustomAttributes(typeof(FlowDirectiveAttribute), true)
                .First() is FlowDirectiveAttribute attr)
            {
                return attr.Path;
            }

            return "";
        }
        public static FlowRoleToken GetRole(Type t)
        {
            if (t.GetCustomAttributes(typeof(FlowDirectiveAttribute), true)
                .First() is FlowDirectiveAttribute attr)
            {
                return attr.Token;
            }

            return FlowRoleToken.None;
        }
    }
}