// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using AutoMapper;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.Data.UnitOfWork;
using AgencyPro.Core.EventHandling;
using AgencyPro.Core.Services;
using Serilog;

namespace AgencyPro.Services
{
    public class Service<TEntity> : IService<TEntity> where TEntity : class, IObjectState
    {
        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[Service.{callerName}] - {message}";
        }

        protected readonly CommandBus CommandBus = new CommandBus();

        protected readonly List<IEvent> Events = new List<IEvent>();
        
        #region Constructor

        public Service(IServiceProvider serviceProvider)
        {
            UnitOfWork = (IUnitOfWorkAsync) serviceProvider.GetService(typeof(IUnitOfWorkAsync));
            ProjectionMapping = (MapperConfiguration) serviceProvider.GetService(typeof(MapperConfiguration));
            Repository = UnitOfWork.RepositoryAsync<TEntity>();
            EventsHandler = new EventsHandler();

            Module = typeof(TEntity).Name;
        }

        #endregion Constructor

        protected MapperConfiguration ProjectionMapping { get; set; }

        IEnumerable<IEvent> IEventSource.GetEvents()
        {
            return Events;
        }

        void IEventSource.Clear()
        {
            Events.Clear();
        }

        public void ClearEvents()
        {
            Events.Clear();
        }

        public void AddEventHandler(params IEventHandler[] handlers)
        {
            EventsHandler.AddEventHandler(handlers);
        }

        public string GetValidationMessage(string id)
        {
            var cmd = new GetValidationMessage {Id = id};
            ExecuteCommand(cmd);
            if (cmd.Message != null) return cmd.Message;

            var result = string.Empty; //ResourceManager.GetString(id, Resources.Culture);
            if (result == null) throw new Exception("Missing validation message for ID : " + id);
            return result;
        }

        public void AddEvent(IEvent evt)
        {
            //evt is IAllowMultiple || 
            if (Events.All(x => x.GetType() != evt.GetType())) Events.Add(evt);
        }

        public void RaiseEvent(IEvent evt)
        {
            Log.Information(GetLogMessage("Raising Event: {0}"), evt);

            //if (evt is IAllowMultiple)
            EventsHandler.EventBus.RaiseEvent(evt);
        }

        public void FireEvents()
        {
            foreach (var ev in Events)
            {
                var ev1 = ev;
                EventsHandler.EventBus.RaiseEvent(ev1);
            }
        }

        public void AddCommandHandler(ICommandHandler handler)
        {
            CommandBus.Add(handler);
        }

        public void ExecuteCommand(ICommand cmd)
        {
            CommandBus.Execute(cmd);
            //Configuration.CommandBus.Execute(cmd);
        }

        #region Private Fields

        protected string Module { get; }
        protected ResourceManager ResourceManager;
        protected IUnitOfWorkAsync UnitOfWork { get; }
        public IRepositoryAsync<TEntity> Repository { get; }
        protected EventsHandler EventsHandler { get; set; }

        #endregion Private Fields
    }
}