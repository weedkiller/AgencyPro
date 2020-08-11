using System.ComponentModel;

namespace IdeaFortune.Mailer
{
    public enum SectionType
    {
        [Description("Marketer")]
        Marketer,

        [Description("Recruiter")]
        Recruiter,

        [Description("Recruiting Director")]
        RecruitingAgencyOwner,

        [Description("Marketing Director")]
        MarketingAgencyOwner,

        [Description("Fulfillment Manager")]
        FulfillmentManager,

        [Description("Agency Owner")]
        AgencyOwner,

        [Description("Contractor")]
        Contractor,

        [Description("Buyer")]
        Customer,

        [Description("Project Manager")]
        ProjectManager,

        [Description("Account Manager")]
        AccountManager,

        [Description("Member")]
        OrganizationPerson,
    }
}