// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationAccountManagers;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationContractors;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationMarketers;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationProjectManagers;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationRecruiters;

namespace AgencyPro.Core.OrganizationPeople.ViewModels
{
    public class AccountManagerOrganizationPersonDetailsOutput : AccountManagerOrganizationPersonOutput
    {
        public AccountManagerOrganizationContractorOutput Contractor { get; set; }
        public AccountManagerOrganizationMarketerOutput Marketer { get; set; }
        public AccountManagerOrganizationRecruiterOutput Recruiter { get; set; }
        public AccountManagerOrganizationProjectManagerOutput ProjectManager { get; set; }
        public AccountManagerOrganizationAccountManagerOutput AccountManager { get; set; }
    }
}