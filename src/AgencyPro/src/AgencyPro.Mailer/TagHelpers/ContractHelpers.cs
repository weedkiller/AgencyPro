using IdeaFortune.Core.Contracts.ViewModels;
using IdeaFortune.Core.EmailSending.Services;
using IdeaFortune.Core.Metadata;
using IdeaFortune.Core.UrlHelpers;
using Microsoft.AspNetCore.Html;

namespace IdeaFortune.Mailer.TagHelpers
{
    public static class ContractHelpers
    {
        

        public static IHtmlContent ContractCreated<T>(this IBasicEmail email,
            T model) where T : ContractOutput, new()
        {
            var role = FlowExtensions.GetRole(typeof(T));

            var message = $"A new contract was added, see the terms below";
            decimal rate = 0;
            string streamName = "CO";
            switch (role)
            {
                case FlowRoleToken.Contractor:
                    rate = model.ContractorStream;
                    streamName = "Hourly Rate (CO Stream)";
                    message = $@"You have been added to a project. The terms of the contract below:" ;
                    break;
                case FlowRoleToken.AgencyOwner:
                    rate = model.AgencyStream;
                    streamName = "PAO Stream";
                    break;
                case FlowRoleToken.AccountManager:
                    rate = model.AccountManagerStream;
                    streamName = "AM Stream";
                    break;
                case FlowRoleToken.ProjectManager:
                    rate = model.ProjectManagerStream;
                    streamName = "PM Stream";
                    break;
                case FlowRoleToken.Customer:
                    rate = model.CustomerRate;
                    streamName = "Hourly Rate";
                    break;
                case FlowRoleToken.Recruiter:
                    rate = model.ContractorStream;
                    streamName = "RE Stream";
                    break;
                case FlowRoleToken.Marketer:
                    rate = model.ContractorStream;
                    message = $"Your contractor, {model.ContractorName}, been added to a project";
                    streamName = "MA Stream";
                    break;
                case FlowRoleToken.MarketingAgencyOwner:
                    rate = model.ContractorStream;
                    streamName = "MAO Stream";
                    break;
                case FlowRoleToken.RecruitingAgencyOwner:
                    rate = model.ContractorStream;
                    streamName = "RAO Stream";
                    break;
            }


            var link = email.GetContractUrl(model);

            var contractDetails = model.ContractDetails(rate, streamName);
            
            return TemplateHelpers.ThreeParagraph(message, link, contractDetails);
        }

        public static IHtmlContent ContractApproved<T>(this IBasicEmail email, 
            T model) where T : ContractOutput, new()
        {
            var role = FlowExtensions.GetRole(typeof(T));

            var message = "";
            switch (role)
            {
                case FlowRoleToken.Contractor:
                    message = $"Your contact was approved, you may start tracking time.";
                    break;

                default:
                    message = "A contract was approved";
                    break;

            }

            var link = email.GetContractUrl(model);

            return TemplateHelpers.TwoParagraph(message, link);
        }

        public static IHtmlContent ContractEnded<T>(this IBasicEmail email, T model) where T : ContractOutput, new()
        {
            var role = FlowExtensions.GetRole(typeof(T));

            var message = "";
            switch (role)
            {

                case FlowRoleToken.Contractor:
                    message = $"Your contact was ended";
                    break;

                default:
                    message = "A contract was ended";
                    break;

            }

            var link = email.GetContractUrl(model);

            return TemplateHelpers.TwoParagraph(message, link);
        }

        public static IHtmlContent ContractPaused<T>(this IBasicEmail email, T model) where T : ContractOutput, new()
        {
            var role = FlowExtensions.GetRole(typeof(T));

            var message = "";
            switch (role)
            {
                case FlowRoleToken.Contractor:
                    message = $"Your contact was paused";
                    break;

                default:
                    message = "A contract was paused";
                    break;

            }

            var link = email.GetContractUrl(model);

            return TemplateHelpers.TwoParagraph(message, link);
        }

        public static IHtmlContent ContractRestarted<T>(this IBasicEmail email, T model) where T : ContractOutput, new()
        {
            var role = FlowExtensions.GetRole(typeof(T));

            var message = "";
            switch (role)
            {
                case FlowRoleToken.Contractor:
                    message = $"Your contact was restarted";
                    break;

                default:
                    message = "A contract was ended";
                    break;

            }

            var link = email.GetContractUrl(model);

            return TemplateHelpers.TwoParagraph(message, link);
        }
    }
}