// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace AgencyPro.Middleware.Providers
{
    public class EmbeddedFileProvider : IFileProvider
    {
        private readonly Assembly _assembly;

        public EmbeddedFileProvider(Assembly assembly)
        {
            _assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            var fullFileName = $"{_assembly.GetName().Name}.{subpath}";

            var isFileEmbedded = _assembly.GetManifestResourceNames().Contains(fullFileName);

            return isFileEmbedded
                ? new EmbeddedFileInfo(subpath, _assembly.GetManifestResourceStream(fullFileName))
                : (IFileInfo) new NotFoundFileInfo(subpath);
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            throw new NotImplementedException();
        }

        public IChangeToken Watch(string filter)
        {
            throw new NotImplementedException();
        }
    }
}