// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Invoices.Emails;
using AgencyPro.Core.Invoices.Models;

namespace AgencyPro.Core.Invoices
{
    public partial class InvoiceProjections
    {
        private void ConfigureEmails()
        {
            CreateMap<ProjectInvoice, AgencyOwnerInvoiceEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.ProviderOrganization.Customer.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.ProviderOrganization.Customer.Person.FirstName))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.ProviderOrganization.Customer.Person.ApplicationUser.Email));


            CreateMap<ProjectInvoice, CustomerInvoiceEmail>()
                .ForMember(x => x.SendMail,
                    opt => opt.MapFrom(x =>
                        x.Customer.Person.ApplicationUser
                            .SendMail))
                .ForMember(x => x.RecipientEmail,
                    opt => opt.MapFrom(x =>
                        x.Customer.Person.ApplicationUser
                            .Email))
                .ForMember(x => x.RecipientName,
                    opt => opt.MapFrom(x =>
                        x.Customer.Person
                            .FirstName)); ;
            
            CreateMap<ProjectInvoice, AccountManagerInvoiceEmail>()
                .ForMember(x => x.SendMail,
                    opt => opt.MapFrom(x =>
                        x.AccountManager.Person.ApplicationUser
                            .SendMail))
                .ForMember(x => x.RecipientEmail,
                    opt => opt.MapFrom(x =>
                        x.AccountManager.Person.ApplicationUser
                            .Email))
                .ForMember(x => x.RecipientName,
                    opt => opt.MapFrom(x =>
                        x.AccountManager.Person.FirstName));
        }
    }
}