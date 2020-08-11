using IdeaFortune.Core.EmailSending.Services;
using IdeaFortune.Core.Metadata;
using IdeaFortune.Core.Projects.ViewModels;
using IdeaFortune.Core.UrlHelpers;
using Microsoft.AspNetCore.Html;

namespace IdeaFortune.Mailer.TagHelpers
{
    public static class ProjectHelpers
    {
        public static IHtmlContent ProjectCreated<T>(this IBasicEmail email, T project) where T : ProjectOutput, new()
        {
            var role = FlowExtensions.GetRole(typeof(T));

            var message = "";
            switch (role)
            {
                default:
                    message = "A new project was created";
                    break;
            }

            var link = email.GetProjectUrl(project);
            var details = project.ProjectDetails();

            return TemplateHelpers.ThreeParagraph(message, link, details);
        }

        public static IHtmlContent ProjectEnded<T>(this IBasicEmail email,
            T project) where T: ProjectOutput, new()
        {

            var role = FlowExtensions.GetRole(typeof(T));

            var message = "";
            switch (role)
            {
                default:
                    message = "A project was ended";
                    break;
            }


            var link = email.GetProjectUrl(project);
            var details = project.ProjectDetails();

            return TemplateHelpers.ThreeParagraph(message, link, details);
        }

        public static IHtmlContent ProjectPaused<T>(this IBasicEmail email,
            T project) where T : ProjectOutput, new()
        {

            var role = FlowExtensions.GetRole(typeof(T));

            var message = "";
            switch (role)
            {
                default:
                    message = "A project was paused";
                    break;
            }


            var link = email.GetProjectUrl(project);
            var details = project.ProjectDetails();

            return TemplateHelpers.ThreeParagraph(message, link, details);
        }

        public static IHtmlContent ProjectRestarted<T>(this IBasicEmail email,
            T project) where T : ProjectOutput, new()
        {

            var role = FlowExtensions.GetRole(typeof(T));

            var message = "";
            switch (role)
            {
                default:
                    message = "A project was restarted";
                    break;
            }


            var link = email.GetProjectUrl(project);
            var details = project.ProjectDetails();

            return TemplateHelpers.ThreeParagraph(message, link, details);
        }
    }
}