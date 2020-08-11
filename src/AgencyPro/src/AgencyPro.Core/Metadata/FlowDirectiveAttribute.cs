// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.Metadata
{
    
    public class FlowDirectiveAttribute : Attribute
    {
        public FlowRoleToken Token { get; }
        public string Path { get; }

        public FlowDirectiveAttribute(FlowRoleToken token, string path)
        {
            Token = token;
            Path = path;
        }
    }
}