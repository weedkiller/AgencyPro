using IdeaFortune.Core.EmailSending.Services;
using IdeaFortune.Core.Metadata;
using IdeaFortune.Core.Stories.ViewModels;
using IdeaFortune.Core.UrlHelpers;
using Microsoft.AspNetCore.Html;

namespace IdeaFortune.Mailer.TagHelpers
{
    public static class StoryHelpers
    {
        public static IHtmlContent StoryCreated<T>(this IBasicEmail email, T story) where T : StoryOutput, new()
        {
            var role = FlowExtensions.GetRole(typeof(T));

            var message = "";
            switch (role)
            {
                case FlowRoleToken.AgencyOwner:
                    message = "A story was created in your organization";
                    break;

                default:
                    message = "A new story was created";
                    break;
            }
            var link = email.GetStoryUrl(story);

            var contractDetails = story.StoryDetails();

            return TemplateHelpers.ThreeParagraph(message, link, contractDetails);

        }
        public static IHtmlContent StoryCompleted<T>(this IBasicEmail email,
            T story) where T : StoryOutput, new()
        {

            var role = FlowExtensions.GetRole(typeof(T));
            var message = "";

            switch (role)
            {
                case FlowRoleToken.AccountManager:
                case FlowRoleToken.ProjectManager:
                case FlowRoleToken.AgencyOwner:
                    message = "A story has been completed";
                    break;

            }
            var link = email.GetStoryUrl(story);

            var contractDetails = story.StoryDetails();

            return TemplateHelpers.ThreeParagraph(message, link, contractDetails);
        }

        public static IHtmlContent StoryAssigned<T>(this IBasicEmail email,
            T story) where T : StoryOutput, new()
        {
            var role = FlowExtensions.GetRole(typeof(T));
            var message = "";

            switch (role)
            {
                case FlowRoleToken.AgencyOwner:
                case FlowRoleToken.AccountManager:
                case FlowRoleToken.ProjectManager:
                    message = "A story has been assigned";
                    break;
                case FlowRoleToken.Contractor:
                    message = "You have been assigned a new Story";
                    break;
            }
            var link = email.GetStoryUrl(story);

            var contractDetails = story.StoryDetails();

            return TemplateHelpers.ThreeParagraph(message, link, contractDetails);
        }
    }
}