// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace AgencyPro.Middleware.Providers
{
    public class EmbeddedFileInfo : IFileInfo
    {
        private readonly Stream _fileStream;

        public EmbeddedFileInfo(string name, Stream fileStream)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (fileStream == null) throw new ArgumentNullException(nameof(fileStream));

            _fileStream = fileStream;

            Exists = true;
            IsDirectory = false;
            Length = fileStream.Length;
            Name = name;
            PhysicalPath = name;
            LastModified = DateTimeOffset.UtcNow;
        }

        public Stream CreateReadStream()
        {
            return _fileStream;
        }

        public bool Exists { get; }
        public bool IsDirectory { get; }
        public long Length { get; }
        public string Name { get; }
        public string PhysicalPath { get; }
        public DateTimeOffset LastModified { get; }
    }
}