// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.UserAccount.Events
{
    public class AccountCreatedEvent : UserAccountEvent
    {
        // InitialPassword might be null if this is a re-send
        // notification for account created (when user tries to
        // reset password before verifying their account)
        public string InitialPassword { get; set; }
        public string VerificationKey { get; set; }
    }
}