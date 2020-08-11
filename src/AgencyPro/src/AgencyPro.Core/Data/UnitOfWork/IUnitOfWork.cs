// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Data.Repositories;

namespace AgencyPro.Core.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        event EventHandler OnSaveChanges;
        int SaveChanges();  
        void Dispose(bool disposing);
        IRepository<TEntity> Repository<TEntity>() where TEntity : class, IObjectState;
        void BeginTransaction(DbIsolationLevel isolationLevel = DbIsolationLevel.Unspecified);
        bool Commit();
        void Rollback();
    }

}