// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Data.DataContext;
using AgencyPro.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AgencyPro.Data.EFCore
{
    public interface IFakeDbContext : IDataContextAsync
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        void AddFakeDbSet<TEntity, TFakeDbSet>() where TEntity : BaseObjectState, new()
            where TFakeDbSet : FakeDbSet<TEntity>, new(); //, IDbSet<TEntity>
    }
}