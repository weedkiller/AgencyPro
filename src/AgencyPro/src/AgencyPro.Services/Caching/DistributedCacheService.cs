// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.Infrastructure.Caching.Services;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace AgencyPro.Services.Caching
{
    #region

    #endregion

    /// <summary>
    ///     Library used for arbitrary caching of objects
    /// </summary>
    public class DistributedCacheService : IDataCache
    {
        private const int Min = 30;
        private static DistributedCacheEntryOptions _policy;
        private readonly IDistributedCache _cache;

        public DistributedCacheService(IDistributedCache distributedCache)
        {
            _cache = distributedCache;
            _policy = new DistributedCacheEntryOptions();
            SetSlidingExpiration(Min);
        }

        public async Task SetAsync<T>(string key, T value, Guid subKey = default)
        {
            var cacheKey = GetKey(key, subKey);
            await _cache.RemoveAsync(key);
            await _cache.SetStringAsync(cacheKey, Stringfy(value), _policy);
        }

        public async Task<T> GetAsync<T>(string key, Guid subKey = default, Func<Task<T>> callback = null)
        {
            var cacheKey = GetKey(key, subKey);
            var stringValue = await _cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(stringValue))
            {
                if (callback != null) await callback();
                return Objectify<T>(stringValue);
            }

            return default;
        }

        public async Task RemoveAsync(string key, Guid subKey = default)
        {
            var cacheKey = GetKey(key, subKey);
            await _cache.RemoveAsync(cacheKey);
        }

        public void SetSlidingExpiration(int min)
        {
            _policy.SlidingExpiration = TimeSpan.FromMinutes(min);
        }

        private string Stringfy<T>(T value)
        {
            return JsonConvert.SerializeObject(value);
        }

        private T Objectify<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        private string GetKey(string key, Guid subKey = default)
        {
            return subKey == default
                ? key
                : string.Format("{0}-{1}", key, subKey);
        }
    }
}