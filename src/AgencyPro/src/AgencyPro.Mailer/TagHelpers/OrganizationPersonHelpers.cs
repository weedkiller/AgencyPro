using IdeaFortune.Core.EmailSending.Services;
using IdeaFortune.Core.OrganizationPeople.ViewModels;
using IdeaFortune.Core.UrlHelpers;
using Microsoft.AspNetCore.Html;

namespace IdeaFortune.Mailer.TagHelpers
{
    public static class OrganizationPersonHelpers
    {
        public static IHtmlContent PersonAdded<T>(this IBasicEmail email, T person)
            where T : OrganizationPersonOutput, new()
        {
            var link = email.GetPersonUrl(person);
            var details = person.PersonDetails();

            return TemplateHelpers.ThreeParagraph("A new person was added to your organization", link, details);
        }
    }
}