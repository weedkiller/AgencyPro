// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;

namespace AgencyPro.Core.Services
{
    public interface IRoleService<in TCreateInput, in TUpdateInput, TOutput, in TPrincipal>
    {
        Task<T> Create<T>(TCreateInput input) where T : TOutput;
        Task<T> GetById<T>(Guid id) where T : TOutput;
        Task<T> Update<T>(TPrincipal principal, TUpdateInput model) where T : TOutput;
        Task<TOutput> Get(Guid id);
    }
}