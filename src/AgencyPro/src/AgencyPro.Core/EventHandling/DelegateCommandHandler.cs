// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.EventHandling
{
    public class DelegateCommandHandler<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        private readonly Action<TCommand> _action;

        public DelegateCommandHandler(Action<TCommand> action)
        {
            _action = action;
        }

        public void Handle(TCommand cmd)
        {
            _action(cmd);
        }
    }
}