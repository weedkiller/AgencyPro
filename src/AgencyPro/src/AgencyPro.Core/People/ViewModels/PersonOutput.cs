// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.People.Enums;
using AgencyPro.Core.People.Services;

namespace AgencyPro.Core.People.ViewModels
{
    public class PersonOutput : PersonInput, IPerson
    {
        public virtual bool IsCustomer { get; set; }
        public virtual bool IsContractor { get; set; }
        public virtual bool IsAccountManager { get; set; }
        public virtual bool IsProjectManager { get; set; }
        public virtual bool IsRecruiter { get; set; }
        public virtual bool IsMarketer { get; set; }
        public virtual Guid Id { get; set; }

        public virtual string ImageUrl { get; set; }
        public virtual string City { get; set; }
        
        public virtual PersonStatus Status { get; set; }
        public virtual DateTimeOffset Created { get; set; }
        public virtual DateTimeOffset Updated { get; set; }
    }
}