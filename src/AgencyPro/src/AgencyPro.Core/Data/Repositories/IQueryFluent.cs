﻿// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AgencyPro.Core.Common;
using AgencyPro.Core.Data.Infrastructure;

namespace AgencyPro.Core.Data.Repositories
{
    public interface IQueryFluent<TEntity> where TEntity : IObjectState
    {
        Task<TResult> FirstOrDefaultAsync<TResult>(Expression<Func<TEntity, object>> selector = null)
            where TResult : class, new();

        IQueryFluent<TEntity> OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
        IQueryFluent<TEntity> Include(Expression<Func<TEntity, object>> expression);
        IQueryFluent<TEntity> Size(int pageSize);
        IQueryFluent<TEntity> Page(int page);

        IEnumerable<TEntity> SelectPage(int page, int pageSize);
        Task<IEnumerable<TEntity>> SelectPageAsync(int page, int pageSize);
        Task<PackedList<TResult>> SelectPageAsync<TResult>(int page, int pageSize) where TResult : new();
        IEnumerable<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> selector = null);
        IEnumerable<TResult> Select<TResult>(Expression<Func<TEntity, object>> selector = null) where TResult : new();
        Task<IEnumerable<TEntity>> SelectAsync(Expression<Func<TEntity, object>> selector);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> selector);

        Task<IEnumerable<TResult>> SelectAsync<TResult>(Expression<Func<TEntity, object>> selector = null)
            where TResult : new();

        Task<PackedList<TResult>> SelectPagedAsync<TResult>(Expression<Func<TEntity, object>> selector = null)
            where TResult : new();

        Task<PackedList<TResult>> SelectPagedAsync<TResult>(int page, int pageSize,
            Expression<Func<TEntity, object>> selector = null) where TResult : new();

        IEnumerable<TEntity> Select();
        Task<IEnumerable<TEntity>> SelectAsync();
        IQueryable<TEntity> SqlQuery(string query, params object[] parameters);
    }
}