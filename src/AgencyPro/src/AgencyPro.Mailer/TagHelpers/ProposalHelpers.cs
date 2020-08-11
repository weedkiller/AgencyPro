using IdeaFortune.Core.EmailSending.Services;
using IdeaFortune.Core.Metadata;
using IdeaFortune.Core.Proposals.ViewModels;
using IdeaFortune.Core.UrlHelpers;
using Microsoft.AspNetCore.Html;

namespace IdeaFortune.Mailer.TagHelpers
{
    public static class ProposalHelpers
    {
        public static IHtmlContent ProposalCreated<T>(this IBasicEmail email,
            T proposal) where T : FixedPriceProposalOutput, new()
        {
            var role = FlowExtensions.GetRole(typeof(T));
            var message = "";
            switch (role)
            {
                case FlowRoleToken.Customer:
                    message = "You have a new proposal";
                    break;
                case FlowRoleToken.AccountManager:
                case FlowRoleToken.ProjectManager:
                    message = "A new proposal was created";
                    break;


                case FlowRoleToken.AgencyOwner:
                    message = "A new proposal has been created in your organization";
                    break;
            }

            var link = email.GetProposalUrl(proposal);
            var details = proposal.ProposalDetails();

            return TemplateHelpers.ThreeParagraph(message, link, details);
        }

        public static IHtmlContent ProposalAccepted<T>(this IBasicEmail email, 
            T proposal) where T : FixedPriceProposalOutput,new()
        {
            var role = FlowExtensions.GetRole(typeof(T));
            var message = "";
            switch (role)
            {
                case FlowRoleToken.Customer:
                    message = "You have accepted the proposal";
                    break;
                case FlowRoleToken.AccountManager:
                case FlowRoleToken.ProjectManager:
                    message = "The proposal was accepted";
                    break;


                case FlowRoleToken.AgencyOwner:
                    message = "A proposal has been accepted by your customer";
                    break;
            }

            var link = email.GetProposalUrl(proposal);
            var details = proposal.ProposalDetails();

            return TemplateHelpers.ThreeParagraph(message, link, details);
        }

        public static IHtmlContent ProposalRejected<T>(this IBasicEmail email,
            T proposal) where T : FixedPriceProposalOutput,new()
        {
            var role = FlowExtensions.GetRole(typeof(T));

            var message = "";
            switch (role)
            {
                case FlowRoleToken.Customer:
                    message = $"You have rejected the proposal from {proposal.ProjectManagerOrganizationName}";
                    break;
                case FlowRoleToken.AccountManager:
                case FlowRoleToken.ProjectManager:
                case FlowRoleToken.AgencyOwner:
                    message = "A proposal has been rejected";
                    break;
            }

            var link = email.GetProposalUrl(proposal);
            var details = proposal.ProposalDetails();

            return TemplateHelpers.ThreeParagraph(message, link, details);
        }


        public static IHtmlContent ProposalSent<T>(this IBasicEmail email,
            T proposal) where T : FixedPriceProposalOutput, new()
        {
            var role = FlowExtensions.GetRole(typeof(T));

            var message = "";
            switch (role)
            {
                case FlowRoleToken.Customer:
                    message = "You have a new proposal";
                    break;
                case FlowRoleToken.AccountManager:
                case FlowRoleToken.ProjectManager:
                    message = "The proposal was sent";
                    break;


                case FlowRoleToken.AgencyOwner:
                    message = "A proposal has sent to your customer";
                    break;
            }
            var link = string.Empty;
            if (role == FlowRoleToken.Customer)
            {
                link = email.GetPublicProposalUrl(proposal);
            }
            else
            {
                link = email.GetProposalUrl(proposal);
            }
            var details = proposal.ProposalDetails();

            return TemplateHelpers.ThreeParagraph(message, link, details);
        }
    }
}