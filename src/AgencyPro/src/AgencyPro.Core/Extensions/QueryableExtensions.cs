// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Common;
using AgencyPro.Core.Models;
using AgencyPro.Core.Organizations.Services;
using Microsoft.EntityFrameworkCore;

namespace AgencyPro.Core.Extensions
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class QueryableExtensions
    {
        public static PaginatedList<T> ToPaginatedList<T>(this IQueryable<T> query, int pageIndex, int pageSize,
            int total)
        {
            var list = query.ToList();
            return new PaginatedList<T>(list, pageIndex, pageSize, total);
        }

        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int? pageIndex, int? pageSize)
        {
            if (pageIndex == null || pageSize == null) return query;
            var skip = (pageIndex.Value - 1) * pageSize.Value;
            return query.Skip(skip).Take(pageSize.Value);
        }

        public async static Task<PackedList<TResult>> PaginateProjection<TEntity, TResult>
            (this IQueryable<TEntity> query, CommonFilters filters, MapperConfiguration mapperConfiguration)
            where TEntity : AuditableEntity where TResult : class
        {
            PackedList<TResult> packedResult = new PackedList<TResult>();

            if (filters.Page.HasValue && filters.PageSize.HasValue)
            {
                var skip = (filters.Page.Value - 1) * filters.PageSize.Value;
                packedResult.Total = await query.ProjectTo<TResult>(mapperConfiguration).CountAsync();
                packedResult.Data = await query.Skip(skip).Take(filters.PageSize.Value)
                    .ProjectTo<TResult>(mapperConfiguration).ToListAsync();
            }
            else
            {
                packedResult.Data = await query.ProjectTo<TResult>(mapperConfiguration).ToListAsync();
                packedResult.Total = packedResult.Data.Count();
            }

            return packedResult;
        }
    }
}