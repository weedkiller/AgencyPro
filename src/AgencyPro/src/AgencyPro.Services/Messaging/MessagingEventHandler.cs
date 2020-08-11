// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Resources;
using AutoMapper;
using AgencyPro.Core.Config;
using AgencyPro.Core.EventHandling;
using Microsoft.Extensions.Options;

namespace AgencyPro.Services.Messaging
{
    public abstract class MessagingEventHandler<TEvent> : IEventHandler
    {
        protected string Module { get; }
        protected ResourceManager ResourceManager;
        protected EventsHandler EventsHandler { get; set; }
        
        protected AppSettings Settings;
        protected MapperConfiguration ProjectionMapping { get; set; }

        protected MessagingEventHandler(IServiceProvider serviceProvider)
        {
            ProjectionMapping = (MapperConfiguration)serviceProvider.GetService(typeof(MapperConfiguration));
            var settings = (IOptions<AppSettings>) serviceProvider.GetService(typeof(IOptions<AppSettings>));
            Settings = settings.Value;
            var evtStr = typeof(TEvent).Name;
            if (!string.IsNullOrEmpty(evtStr)) Module = evtStr.Replace("Event", "");
        }

        protected string GetEvent(TEvent t)
        {
            var evtStr = GetEventName(t).Replace(Module, "");
            return evtStr;
        }

        protected string GetEventName(TEvent t)
        {
            var evtStr = t.GetType().Name;
            if (!string.IsNullOrEmpty(evtStr)) evtStr = evtStr.Replace("Event", "");
            return evtStr;
        }
    }
}