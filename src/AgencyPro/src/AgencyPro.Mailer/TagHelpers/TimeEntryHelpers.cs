using IdeaFortune.Core.EmailSending.Services;
using IdeaFortune.Core.Metadata;
using IdeaFortune.Core.TimeEntries.ViewModels;
using IdeaFortune.Core.UrlHelpers;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IdeaFortune.Mailer.TagHelpers
{
    public static class TimeEntryHelpers
    {
        public static IHtmlContent TimeEntryCreated<T>(this IBasicEmail email,
            T entry) where T : TimeEntryOutput, new()
        {
            var role = FlowExtensions.GetRole(typeof(T));

            var message = "";
            var streamName = "";
            var stream = 0m;
            
            switch (role)
            {
                case FlowRoleToken.Contractor:
                    stream = entry.TotalContractorStream;
                    streamName = "CO Stream";
                    message = $"You logged {entry.TotalHours.ToString("N2")} Hours";
                    break;
                case FlowRoleToken.AccountManager:
                    stream = entry.TotalAccountManagerStream;
                    streamName = "AM Stream";
                    message = $"A time entry of {entry.TotalHours.ToString("N2")} Hours was logged";
                    break;
                case FlowRoleToken.ProjectManager:
                    stream = entry.TotalProjectManagerStream;
                    streamName = "PM Stream";
                    message = $"A time entry of {entry.TotalHours.ToString("N2")} Hours was logged";
                    break;
                case FlowRoleToken.AgencyOwner:
                    stream = entry.TotalAgencyStream;
                    streamName = "AO Stream";
                    message = $"A time entry of {entry.TotalHours.ToString("N2")} Hours was logged";
                    break;
                case FlowRoleToken.Customer:
                    stream = entry.TotalCustomerAmount;
                    streamName = "Amount";
                    message = $"A time entry of {entry.TotalHours.ToString("N2")} Hours was logged";
                    break;
                case FlowRoleToken.Recruiter:
                    stream = entry.TotalRecruiterStream;
                    streamName = "RE Stream";
                    message = $"A time entry of {entry.TotalHours.ToString("N2")} Hours was logged";
                    break;
                case FlowRoleToken.Marketer:
                    stream = entry.TotalMarketerStream;
                    streamName = "MA Stream";
                    message = $"A time entry of {entry.TotalHours.ToString("N2")} Hours was logged";
                    break;
                case FlowRoleToken.MarketingAgencyOwner:
                    stream = entry.TotalMarketingAgencyStream;
                    streamName = "MAO Stream";
                    message = $"A time entry of {entry.TotalHours.ToString("N2")} Hours was logged";
                    break;
                case FlowRoleToken.RecruitingAgencyOwner:
                    stream = entry.TotalRecruitingAgencyStream;
                    streamName = "RAO Stream";
                    message = $"A time entry of {entry.TotalHours.ToString("N2")} Hours was logged";
                    break;
            }

            var link = email.GetTimeEntryUrl(entry);
            var timeDetails = entry.TimeEntryDetails(stream, streamName);

            return TemplateHelpers.ThreeParagraph(message, link, timeDetails);
        }
        public static IHtmlContent TimeEntryApproved<T>(this IBasicEmail email,
            T entry) where T : TimeEntryOutput, new()
        {
            var role = FlowExtensions.GetRole(typeof(T));

            var message = "";
            var streamName = "";
            var stream = 0m;
            switch (role)
            {
                case FlowRoleToken.Contractor:
                    stream = entry.TotalContractorStream;
                    streamName = "CO Stream";
                    message = $"Your time entry was approved";
                    break;
                case FlowRoleToken.AccountManager:
                    stream = entry.TotalAccountManagerStream;
                    streamName = "AM Stream";
                    message = $"A time entry of {entry.TotalHours.ToString("N2")} Hours was approved";
                    break;
                case FlowRoleToken.ProjectManager:
                    stream = entry.TotalProjectManagerStream;
                    streamName = "PM Stream";
                    message = $"A time entry of {entry.TotalHours.ToString("N2")} Hours was approved";
                    break;
                case FlowRoleToken.AgencyOwner:
                    stream = entry.TotalAgencyStream;
                    streamName = "AO Stream";
                    message = $"A time entry of {entry.TotalHours.ToString("N2")} Hours was approved";
                    break;
                case FlowRoleToken.Customer:
                    stream = entry.TotalCustomerAmount;
                    streamName = "Amount";
                    message = $"A time entry of {entry.TotalHours.ToString("N2")} Hours was approved";
                    break;
                case FlowRoleToken.Recruiter:
                    stream = entry.TotalRecruiterStream;
                    streamName = "RE Stream";
                    message = $"A time entry of {entry.TotalHours.ToString("N2")} Hours was approved";
                    break;
                case FlowRoleToken.Marketer:
                    stream = entry.TotalMarketerStream;
                    streamName = "MA Stream";
                    message = $"A time entry of {entry.TotalHours.ToString("N2")} Hours was approved";
                    break;
                case FlowRoleToken.MarketingAgencyOwner:
                    stream = entry.TotalMarketingAgencyStream;
                    streamName = "MAO Stream";
                    message = $"A time entry of {entry.TotalHours.ToString("N2")} Hours was approved";
                    break;
                case FlowRoleToken.RecruitingAgencyOwner:
                    stream = entry.TotalRecruitingAgencyStream;
                    streamName = "RAO Stream";
                    message = $"A time entry of {entry.TotalHours.ToString("N2")} Hours was approved";
                    break;
            }

            var link = email.GetTimeEntryUrl(entry);
            var timeDetails = entry.TimeEntryDetails(stream, streamName);

            return TemplateHelpers.ThreeParagraph(message, link, timeDetails);
        }
        public static IHtmlContent TimeEntryRejected(this IBasicEmail email, SectionType sectionType,
            TimeEntryOutput entry)
        {
            var message = "";
            switch (sectionType)
            {
                case SectionType.AgencyOwner:
                case SectionType.AccountManager:
                case SectionType.ProjectManager:
                    message = "A time entry was rejected";
                    break;


                case SectionType.Contractor:
                    message = $"Your time entry was rejected for the following reason: { entry.RejectionReason }";
                    break;
            }

            var root = new TagBuilder("div");
            root.InnerHtml.Append(message);

            return root;
        }
    }
}