// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Models;
using AgencyPro.Core.Notifications.Models;
using AgencyPro.Core.People.Models;
using Microsoft.AspNetCore.Identity;

namespace AgencyPro.Core.UserAccount.Models
{
   
    public class ApplicationUser : IdentityUser<Guid>, IObjectState
    {
        public ApplicationUser()
        {
            this.UserClaims = new List<IdentityUserClaim<Guid>>();
            this.UserTokens = new List<IdentityUserToken<Guid>>();
            this.UserLogins = new List<IdentityUserLogin<Guid>>();
        }

        public virtual DateTimeOffset Created { get; set; }
        public virtual DateTimeOffset LastUpdated { get; set; }
        public virtual DateTimeOffset? PasswordChanged { get; set; }

        public virtual Person Person { get; set; }

        public virtual ICollection<ExceptionLog.ExceptionLog> ExceptionsRaised { get; set; }
        public virtual ICollection<AuditLog> AuditLogs { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<UserNotification> Notifications { get; set; }

        public virtual ICollection<IdentityUserToken<Guid>> UserTokens { get; set; }
        public virtual ICollection<IdentityUserClaim<Guid>> UserClaims { get; set; }
        public virtual ICollection<IdentityUserLogin<Guid>> UserLogins { get; set; }

        public virtual DateTimeOffset? LastLogin { get; set; }

        public ObjectState ObjectState { get; set; }

        public bool IsAdmin { get; set; }


        public void AddClaim(IdentityUserClaim<Guid> item)
        {
            item.UserId = Id;
            UserClaims.Add(item);
        }

        public void RemoveClaim(IdentityUserClaim<Guid> item)
        {
            UserClaims.Remove(item);
        }


        public bool SendMail { get; set; }
    }
}