// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;

namespace AgencyPro.Core.Infrastructure.Caching.Services
{
    public interface IDataCache
    {
        Task SetAsync<T>(string key, T value, Guid subKey = default);
        Task<T> GetAsync<T>(string key, Guid subKey = default, Func<Task<T>> callback = null);
        Task RemoveAsync(string key, Guid subKey = default);
    }
}