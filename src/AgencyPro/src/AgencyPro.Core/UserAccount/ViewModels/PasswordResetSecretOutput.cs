﻿// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.UserAccount.ViewModels
{
    public class PasswordResetSecretOutput
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }

        public string Question { get; set; }
        //public string Answer { get; set; }
        //public Guid UserId { get; set; }
    }
}