// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationAccountManagers;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationContractors;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationMarketers;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationProjectManagers;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationRecruiters;

namespace AgencyPro.Core.OrganizationPeople.ViewModels
{
    public class AgencyOwnerOrganizationPersonDetailsOutput : AgencyOwnerOrganizationPersonOutput
    {
        public AgencyOwnerOrganizationContractorOutput Contractor { get; set; }
        public AgencyOwnerOrganizationMarketerOutput Marketer { get; set; }
        public AgencyOwnerOrganizationRecruiterOutput Recruiter { get; set; }
        public AgencyOwnerOrganizationProjectManagerOutput ProjectManager { get; set; }
        public AgencyOwnerOrganizationAccountManagerOutput AccountManager { get; set; }
    }
}