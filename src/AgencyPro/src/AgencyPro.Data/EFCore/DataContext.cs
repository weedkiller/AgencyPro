// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Data.DataContext;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Data.UnitOfWork;
using AgencyPro.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AgencyPro.Core.UserAccount.Models;
using Microsoft.AspNetCore.Identity;

namespace AgencyPro.Data.EFCore
{
    public class DataContext<TContext> : 
        IdentityDbContext<
            ApplicationUser, Role, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>,
            IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
        , IDataContextAsync where TContext : DbContext
    {
        protected DataContext(DbContextOptions<TContext> options) : base(options)
        {
            if (_databaseInitialized) return;
            lock (_lock)
            {
                if (_databaseInitialized) return;
                InstanceId = Guid.NewGuid();
                _databaseInitialized = true;
            }
        }

        public Guid InstanceId { get; }

        public object GetKey<TEntity>(TEntity entity)
        {
            var entityType = Model.FindEntityType(entity.GetType());
            if (entityType == null) return default(int?);

            var entityKeys = entityType.FindPrimaryKey();
            var keyName = entityKeys.Properties.Select(x => x.Name).FirstOrDefault();
            return entity.GetType().GetProperty(keyName).GetValue(entity, null);
        }

        /// <summary>
        ///     Saves all changes made in this context to the underlying database.
        ///     An error occurred sending updates to the database.
        ///     A database command did not affect the expected number of rows. This usually
        ///     indicates an optimistic concurrency violation; that is, a row has been changed
        ///     in the database since it was queried.
        ///     An attempt was made to use unsupported behavior such as executing multiple
        ///     asynchronous commands concurrently on the same context instance.
        ///     <exception cref="ObjectDisposedException">
        ///         The context or connection have been disposed.
        ///     </exception>
        ///     <exception cref="InvalidOperationException">
        ///         Some error occurred attempting to process entities in the context either
        ///         before or after sending commands to the database.
        ///     </exception>
        ///     <seealso cref="DbContext.SaveChanges" />
        ///     <returns>The number of objects written to the underlying database.</returns>
        /// </summary>
        public override int SaveChanges()
        {
            var validationErrors =
                ChangeTracker.Entries<IValidatableObject>()
                    .SelectMany(e => e.Entity.Validate(null))
                    .Where(r => r != ValidationResult.Success)
                    .ToList();

            if (validationErrors.Any())
            {
                var errors = validationErrors.ToDictionary(kvp => kvp.MemberNames, kvp => kvp.ErrorMessage)
                    .Where(m => m.Value.Any());
                var err = string.Join(",", errors.Select(i => i.Value.ToString()).ToArray());
                throw new DbUpdateException(err, (Exception)null);
            }
            // Could also be before try if you know the exception occurs in SaveChanges

            SyncObjectsStatePreCommit();
            var changes = base.SaveChanges();
            SyncObjectsStatePostCommit();
            return changes;
        }

        /// <summary>
        ///     Asynchronously saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>
        ///     A task that represents the asynchronous save operation.  The
        ///     Task.Result contains the number of objects written to the underlying database.
        /// </returns>
        /// exceDbUpdateExceptionateException: An error occurred sending updates to the database./>
        /// exceDbUpdateConcurrencyExceptionncyException: A database command did not affect the expected number of rows. This usually
        /// indicates an optimistic concurrency violation; that is, a row has been changed
        /// in the database since it was queried.
        /// <exception cref="System.NotSupportedException">
        ///     An attempt was made to use unsupported behavior such as executing multiple
        ///     asynchronous commands concurrently on the same context instance.
        /// </exception>
        /// <exception cref="System.ObjectDisposedException">The context or connection have been disposed.</exception>
        /// <exception cref="System.InvalidOperationException">
        ///     Some error occurred attempting to process entities in the context either
        ///     before or after sending commands to the database.
        /// </exception>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <seealso cref="DbContext.SaveChangesAsync" />
        public async Task<int> SaveChangesAsync()
        {
            return await SaveChangesAsync(CancellationToken.None);
        }

        public Task ExecuteSqlAsync(string query, CancellationToken token, object[] parameters)
        {
            return Database.ExecuteSqlCommandAsync(query, token, parameters);
        }

        /// <summary>
        ///     Asynchronously saves all changes made in this context to the database.
        /// </summary>
        /// <param name="cancellationToken">
        ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the
        ///     task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous save operation. The task result contains the
        ///     number of state entries written to the database.
        /// </returns>
        /// <remarks>
        ///     <para>
        ///         This method will automatically call
        ///         <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges" /> to discover any
        ///         changes to entity instances before saving to the underlying database. This can be disabled via
        ///         <see cref="P:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled" />.
        ///     </para>
        ///     <para>
        ///         Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///         that any asynchronous operations have completed before calling another method on this context.
        ///     </para>
        /// </remarks>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            await SyncObjectsStatePreCommitAsync();
            var changesAsync = await base.SaveChangesAsync(cancellationToken);
            await SyncObjectsStatePostCommitAsync();
            return changesAsync;
        }

        public void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState
        {
            Entry(entity).State = StateHelper.ConvertState(entity.ObjectState);
        }

        public void SyncObjectsStatePostCommit()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entityEntry in entries)
                ((IObjectState)entityEntry.Entity).ObjectState = StateHelper.ConvertState(entityEntry.State);
            //Audit(entityEntry);
        }

        /// <summary>
        ///     Synchronizes the objects state post commit asynchronous.
        /// </summary>
        /// <returns></returns>
#pragma warning disable 1998
        public async Task SyncObjectsStatePostCommitAsync()
#pragma warning restore 1998
        {
            foreach (var entityEntry in ChangeTracker.Entries())
                ((IObjectState)entityEntry.Entity).ObjectState = StateHelper.ConvertState(entityEntry.State);
            // await AuditAsync(entityEntry);
        }

        public IDbTransaction BeginTransaction(DbIsolationLevel isolationLevel)
        {
            return (IDbTransaction)Database.BeginTransaction();
        }

        public Guid? GetGuidKey<TEntity>(TEntity entity)
        {
            var entityType = Model.FindEntityType(entity.GetType());
            if (entityType == null) return default;

            var entityKeys = entityType.FindPrimaryKey();
            var keyName = entityKeys.Properties.Select(x => x.Name).FirstOrDefault();
            var value = entity.GetType().GetProperty(keyName).GetValue(entity, null);
            return (Guid?)value;
        }

        public Task BeginTransactionAsync(DbIsolationLevel isolationLevel)
        {
            throw new NotImplementedException();
        }

        private void SyncObjectsStatePreCommit()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entityEntry in entries)
                entityEntry.State = StateHelper.ConvertState(((IObjectState)entityEntry.Entity).ObjectState);
            //Audit(entityEntry);
        }

        /// <summary>
        ///     Synchronizes the objects state pre commit asynchronous.
        /// </summary>
        /// <returns></returns>
#pragma warning disable 1998
        private async Task SyncObjectsStatePreCommitAsync()
#pragma warning restore 1998
        {
            foreach (var entityEntry in ChangeTracker.Entries())
                entityEntry.State = StateHelper.ConvertState(((IObjectState)entityEntry.Entity).ObjectState);
            // await AuditAsync(entityEntry);
        }

        private AuditLog GetAudit(EntityEntry entry)
        {
            var entityId = GetKey(entry.Entity);
            return new AuditLog
            {
                EntityId = entityId != null ? entityId.ToString() : "",
                EntityType = entry.Entity.GetType().Name,
                Event = entry.State.ToString(),
                ObjectState = ObjectState.Added,
                UserId = null //_userInfo.UserId
            };
        }

        private async Task AuditAsync(EntityEntry entry)
        {
            var newAuditLog = GetAudit(entry);
            await Task.Run(() =>
            {
                Set<AuditLog>().Add(newAuditLog);
                base.SaveChangesAsync();
            });
        }

        private void Audit(EntityEntry entry)
        {
            var newAuditLog = GetAudit(entry);
            Set<AuditLog>().Add(newAuditLog);
            base.SaveChanges();
        }

        #region Private Fields

        private readonly bool _databaseInitialized;
        private readonly object _lock = new object();

        #endregion Private Fields
    }
}