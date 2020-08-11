// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.EventHandling;

namespace AgencyPro.Core.Services
{
    public interface IService<TEntity> : IEventSource where TEntity : class, IObjectState
    {
        IRepositoryAsync<TEntity> Repository { get; }
        void AddEvent(IEvent evt);
        void RaiseEvent(IEvent evt);
        void FireEvents();
        void ClearEvents();
        string GetValidationMessage(string id);
        void AddEventHandler(params IEventHandler[] handlers);
    }
}