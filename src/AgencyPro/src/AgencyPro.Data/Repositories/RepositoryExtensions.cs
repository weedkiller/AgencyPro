// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;

namespace AgencyPro.Data.Repositories
{
    public static class Extensions
    {
        public static TEntity Find<TEntity>(this DbSet<TEntity> set, params object[] keyValues) where TEntity : class
        {
            var context = set.GetService<ICurrentDbContext>().Context;

            var entityType = context.Model.FindEntityType(typeof(TEntity));
            var key = entityType.FindPrimaryKey();

            var entries = context.ChangeTracker.Entries<TEntity>();

            var i = 0;
            foreach (var property in key.Properties)
            {
                entries = entries.Where(e => e.Property(property.Name).CurrentValue == keyValues[i]);
                i++;
            }

            var entry = entries.FirstOrDefault();
            if (entry != null) return entry.Entity;

            // TODO: Build the real LINQ Expression
            // set.Where(x => x.Id == keyValues[0]);
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var query = set.Where((Expression<Func<TEntity, bool>>)
                Expression.Lambda(
                    Expression.Equal(
                        Expression.Property(parameter, "Id"),
                        Expression.Constant(keyValues[0])),
                    parameter));

            // Look in the database
            return query.FirstOrDefault();
        }

        public static Task<TEntity> FindAsync<TEntity>(this DbSet<TEntity> set, params object[] keyValues)
            where TEntity : class
        {
            var context = set.GetService<ICurrentDbContext>().Context;

            var entityType = context.Model.FindEntityType(typeof(TEntity));
            var key = entityType.FindPrimaryKey();

            var entries = context.ChangeTracker.Entries<TEntity>();

            var i = 0;
            foreach (var property in key.Properties)
            {
                entries = entries.Where(e => e.Property(property.Name).CurrentValue == keyValues[i]);
                i++;
            }

            var entry = entries.FirstOrDefault();
            if (entry != null) return Task.FromResult(entry.Entity);

            // TODO: Build the real LINQ Expression
            // set.Where(x => x.Id == keyValues[0]);
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var query = set.Where((Expression<Func<TEntity, bool>>)
                Expression.Lambda(
                    Expression.Equal(
                        Expression.Property(parameter, "Id"),
                        Expression.Constant(keyValues[0])),
                    parameter));

            // Look in the database
            return query.FirstOrDefaultAsync();
        }
    }
}