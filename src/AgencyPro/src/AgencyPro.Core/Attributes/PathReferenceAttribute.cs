// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core
{
    /// <summary>
    ///     Indicates that a parameter is a path to a file or a folder within a web project.
    ///     Path can be relative or absolute, starting from web root (~).
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class PathReferenceAttribute : Attribute
    {
        public PathReferenceAttribute()
        {
        }

        public PathReferenceAttribute([NotNull] [PathReference] string basePath)
        {
            BasePath = basePath;
        }

        [CanBeNull] public string BasePath { get; }
    }
}