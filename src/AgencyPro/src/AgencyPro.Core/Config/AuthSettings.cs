// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.Config
{
    public class AuthSettings
    {
        public bool EmailIsUsername { get; set; }
        public bool RequireAccountVerification { get; set; }
        public bool AllowLoginAfterAccountCreation { get; set; }
        public int AccountLockoutFailedLoginAttempts { get; set; }
        public TimeSpan AccountLockoutDuration { get; set; }
        public bool AllowAccountDeletion { get; set; }
        public int MinimumPasswordLength { get; set; }
        public int PasswordResetFrequency { get; set; }
        public int PasswordHashingIterationCount { get; set; }
        public int MobileVerificationCodeLength { get; set; }
        public int MobileCodeStaleDurationMinutes { get; set; }
        public int MobileCodeResendDelayMinutes { get; set; }
        public int TwoFactorAuthTokenDurationDays { get; set; }

        public bool AllowEmailChangeWhenEmailIsUsername { get; set; }
        public bool AllowMultipleMobileLogin { get; set; }
        public TimeSpan VerificationKeyLifetime { get; set; }
        public bool EmailIsUnique { get; set; }
    }
}