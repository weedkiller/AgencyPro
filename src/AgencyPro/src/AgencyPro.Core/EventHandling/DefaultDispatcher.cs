// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AgencyPro.Core.EventHandling
{
    public class DefaultDispatcher : IEventDispatcher
    {
        private readonly Dictionary<Type, Collection<Delegate>> _handlers;

        public DefaultDispatcher()
        {
            _handlers = new Dictionary<Type, Collection<Delegate>>();
        }

        public void Resolve()
        {
            throw new NotImplementedException();
        }

        public void Dispatch<TEvent>(TEvent e)
        {
            Collection<Delegate> eventHandlers;
            if (!_handlers.TryGetValue(typeof(TEvent), out eventHandlers)) return;
            foreach (var handler in eventHandlers.Cast<Action<TEvent>>())
                try
                {
                    handler(e);
                }
                catch
                {
                    // log
                }
        }

        public void Register<TEvent>(Action<TEvent> handler)
        {
            Collection<Delegate> eventHandlers;
            if (!_handlers.TryGetValue(typeof(TEvent), out eventHandlers))
            {
                eventHandlers = new Collection<Delegate>();
                _handlers.Add(typeof(TEvent), eventHandlers);
            }

            eventHandlers.Add(handler);
        }
    }
}