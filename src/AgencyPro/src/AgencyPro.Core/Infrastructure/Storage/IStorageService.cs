// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AgencyPro.Core.Infrastructure.Storage
{
    public interface IStorageService
    {
        Task<string> StorePngImageAtPath(
            IFormFile image, string container, string path);
    }
}