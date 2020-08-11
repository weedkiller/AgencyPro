using IdeaFortune.Core.EmailSending.Services;
using IdeaFortune.Core.Candidates.ViewModels;
using IdeaFortune.Core.Metadata;
using IdeaFortune.Core.UrlHelpers;
using Microsoft.AspNetCore.Html;

namespace IdeaFortune.Mailer.TagHelpers
{
    public static class CandidateHelpers
    {
        public static IHtmlContent CandidateCreated<T>(this IBasicEmail email, T model) 
            where T : CandidateOutput, new()
        {

            var role = FlowExtensions.GetRole(typeof(T));


            var message = "";
            var streamName = "";
            var stream = 0m;
            var bonus = 0m;
            var bonusName = "";
            
            switch (role)
            {
                case FlowRoleToken.Recruiter:
                    bonus = model.RecruiterBonus;
                    stream = model.RecruiterStream;
                    streamName = "RE Stream";
                    bonusName = "RE Bonus";
                    message = "A new candidate was created";
                    break;

                case FlowRoleToken.RecruitingAgencyOwner:
                    bonus = model.RecruitingAgencyBonus;
                    stream = model.RecruitingAgencyStream;
                    streamName = "RAO Stream";
                    bonusName = "RAO Bonus";
                    message = "A new candidate was created by one of your organization's recruiters";
                    break;
                case FlowRoleToken.AgencyOwner:
                    bonus = model.RecruitingBonus;
                    stream = model.RecruitingStream;
                    streamName = "Recruiting Stream";
                    bonusName = "Recruiting Bonus";
                    message = "You have a new candidate ready to be qualified";
                    break;
            }
            var link = email.GetCandidateUrl(model);

            var contractDetails = model.CandidateDetails(stream, streamName, bonus, bonusName);

            return TemplateHelpers.ThreeParagraph(message, link, contractDetails);
        }
        public static IHtmlContent CandidateQualified<T>(this IBasicEmail email, T model) where  T : CandidateOutput, new()
        {
            var role = FlowExtensions.GetRole(typeof(T));

            var message = "";
            var streamName = "";
            var stream = 0m;
            var bonus = 0m;
            var bonusName = "";

            switch (role)
            {
                case FlowRoleToken.Recruiter:
                    bonus = model.RecruiterBonus;
                    stream = model.RecruiterStream;
                    streamName = "RE Stream";
                    bonusName = "RE Bonus";
                    message = "Your candidate was qualified";
                    break;

                case FlowRoleToken.RecruitingAgencyOwner:
                    bonus = model.RecruitingAgencyBonus;
                    stream = model.RecruitingAgencyStream;
                    streamName = "RAO Stream";
                    bonusName = "RAO Bonus";
                    message = "Your candidate was qualified";
                    break;
                case FlowRoleToken.AgencyOwner:
                    bonus = model.RecruitingBonus;
                    stream = model.RecruitingStream;
                    streamName = "Recruiting Stream";
                    bonusName = "Recruiting Bonus";
                    message = "Your candidate was successfully assigned";
                    break;
                case FlowRoleToken.ProjectManager:
                    bonus = 0;
                    stream = 0;
                    streamName = "Recruiting Stream";
                    bonusName = "Recruiting Bonus";
                    message = "A new Candidate was assigned to you for screening.";
                    break;
            }

            var link = email.GetCandidateUrl(model);

            var contractDetails = model.CandidateDetails(stream, streamName, bonus, bonusName);

            return TemplateHelpers.ThreeParagraph(message, link, contractDetails);
        }

        public static IHtmlContent CandidateRejected<T>(this IBasicEmail email, T model) where T : CandidateOutput, new()
        {
            var role = FlowExtensions.GetRole(typeof(T));

            var message = "";
            switch (role)
            {
                case FlowRoleToken.Recruiter:
                    message = "Your candidate was rejected";
                    break;

                case FlowRoleToken.RecruitingAgencyOwner:
                    message = "Your candidate was rejected";
                    break;
                case FlowRoleToken.AgencyOwner:
                    message = "Your candidate was rejected";
                    break;
                case FlowRoleToken.AccountManager:
                    message = "Your candidate was rejected";
                    break;
            }

            var link = email.GetCandidateUrl(model);

            return TemplateHelpers.TwoParagraph(message, link);
        }
    }
}