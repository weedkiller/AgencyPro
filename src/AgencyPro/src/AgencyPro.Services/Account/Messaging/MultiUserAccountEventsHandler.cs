// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.EventHandling;
using AgencyPro.Core.UserAccount.Events;
using AgencyPro.Services.Messaging.Email;

namespace AgencyPro.Services.Account.Messaging
{
    public class MultiUserAccountEventsHandler : MultiEventHandler<UserAccountEvent>,
        IEventHandler<AccountCreatedEvent>,
        IEventHandler<MobileVerifiedEvent>,
        IEventHandler<AccountClosedEvent>,
        IEventHandler<AccountReopenedEvent>,
        IEventHandler<EmailChangeRequestedEvent>,
        IEventHandler<EmailChangedEvent>,
        IEventHandler<EmailVerifiedEvent>,
        IEventHandler<MobilePhoneChangedEvent>,
        IEventHandler<MobilePhoneChangeRequestedEvent>,
        IEventHandler<MobilePhoneRemovedEvent>,
        IEventHandler<UsernameReminderRequestedEvent>,
        IEventHandler<UsernameChangedEvent>
    {
        public MultiUserAccountEventsHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }


        public void Handle(AccountClosedEvent evt)
        {
            Process(evt);
        }

        public void Handle(AccountCreatedEvent evt)
        {
            Process(evt);
        }

        public void Handle(AccountReopenedEvent evt)
        {
            Process(evt);
        }

        public void Handle(EmailChangedEvent evt)
        {
            Process(evt);
        }

        public void Handle(EmailChangeRequestedEvent evt)
        {
            Process(evt);
        }

        public void Handle(EmailVerifiedEvent evt)
        {
            Process(evt);
        }

        public void Handle(MobilePhoneChangedEvent evt)
        {
            Process(evt);
        }

        public void Handle(MobilePhoneChangeRequestedEvent evt)
        {
            Process(evt);
        }

        public void Handle(MobilePhoneRemovedEvent evt)
        {
            Process(evt);
        }

        public void Handle(MobileVerifiedEvent evt)
        {
            Process(evt);
        }
        

        public void Handle(UsernameChangedEvent evt)
        {
            Process(evt);
        }

        public void Handle(UsernameReminderRequestedEvent evt)
        {
            Process(evt);
        }

        private void Process(UserAccountEvent evt)
        {
            //evt.AppInfo = Settings.Information;
            //evt.Urls = Settings.Urls;
            //Send(evt, evt.Account.Email);
        }
    }
}