// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace AgencyPro.Core.EventHandling
{
    public class CommandBus : List<ICommandHandler>, ICommandBus
    {
        private readonly GenericMethodActionBuilder<ICommandHandler, ICommand> _actions =
            new GenericMethodActionBuilder<ICommandHandler, ICommand>(typeof(ICommandHandler<>), "Handle");

        private readonly ConcurrentDictionary<Type, IEnumerable<ICommandHandler>> _handlerCache =
            new ConcurrentDictionary<Type, IEnumerable<ICommandHandler>>();

        public void Execute(ICommand cmd)
        {
            var action = GetAction(cmd);
            var matchingHandlers = GetHandlers(cmd);
            foreach (var handler in matchingHandlers) action(handler, cmd);
        }

        private Action<ICommandHandler, ICommand> GetAction(ICommand evt)
        {
            return _actions.GetAction(evt);
        }

        private IEnumerable<ICommandHandler> GetHandlers(ICommand cmd)
        {
            var eventType = cmd.GetType();
            if (_handlerCache.ContainsKey(eventType)) return _handlerCache[eventType];
            var eventHandlerType = typeof(ICommandHandler<>).MakeGenericType(eventType);
            var query =
                from handler in this
                where eventHandlerType == handler.GetType()
                select handler;
            var handlers = query.ToArray().Cast<ICommandHandler>();
            _handlerCache[eventType] = handlers;
            return _handlerCache[eventType];
        }
    }
}