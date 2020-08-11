// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Concurrent;

namespace AgencyPro.Core.EventHandling
{
    public class DeferredEventDispatcher : IEventDispatcher
    {
        private readonly ConcurrentQueue<Action> _events = new ConcurrentQueue<Action>();
        private readonly IEventDispatcher _inner;

        public DeferredEventDispatcher(IEventDispatcher inner)
        {
            _inner = inner;
        }

        public void Dispatch<TEvent>(TEvent e)
        {
            _events.Enqueue(() => _inner.Dispatch(e));
        }

        public void Resolve()
        {
            Action dispatch;
            while (_events.TryDequeue(out dispatch)) dispatch();
        }
    }
}