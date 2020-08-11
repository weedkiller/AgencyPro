// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Models;
using AgencyPro.Core.UserAccount.Models;

namespace AgencyPro.Core.ExceptionLog
{
    public class ExceptionLog : BaseObjectState
    {
        public int Id { get; set; }
        public int HResult { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string RequestUri { get; set; }
        public string Method { get; set; }
        public string StackTrace { get; set; }
        public Guid? UserId { get; set; }
        public DateTimeOffset Created { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}