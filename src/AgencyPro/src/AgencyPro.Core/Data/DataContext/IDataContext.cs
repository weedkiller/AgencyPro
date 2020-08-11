// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Data.Infrastructure;

namespace AgencyPro.Core.Data.DataContext
{
    public interface IDataContext : IDisposable
    {
        //IDbTransaction BeginTransaction(DbIsolationLevel isolationLevel);
        object GetKey<TEntity>(TEntity entity);
        int SaveChanges();
        void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState;
        void SyncObjectsStatePostCommit();
    }
}