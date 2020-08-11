// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Orders.Emails;
using AgencyPro.Core.Orders.Model;

namespace AgencyPro.Core.Orders
{
    public partial class WorkOrderProjections
    {
        private void EmailProjections()
        {
            CreateMap<WorkOrder, AccountManagerWorkOrderEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.AccountManager.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.AccountManager.Person.ApplicationUser.Email))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.AccountManager.Person.DisplayName));

            CreateMap<WorkOrder, AgencyOwnerWorkOrderEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.OrganizationAccountManager.Organization.Customer.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.OrganizationAccountManager.Organization.Customer.Person.ApplicationUser.Email))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.OrganizationAccountManager.Organization.Customer.Person.DisplayName));

            CreateMap<WorkOrder, CustomerWorkOrderEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.BuyerOrganization.Customer.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.BuyerOrganization.Customer.Person.ApplicationUser.Email))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.BuyerOrganization.Customer.Person.DisplayName));

        }
    }
}