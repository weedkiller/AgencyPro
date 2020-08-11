using IdeaFortune.Core;
using IdeaFortune.Core.EmailSending.Services;
using IdeaFortune.Core.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IdeaFortune.Mailer.TagHelpers
{
    public static class TagHelpers
    {
        public static IHtmlContent Introduction(this IBasicEmail email)
        {
            var span = new TagBuilder("p");

            span.InnerHtml.Append($"Hello {email.RecipientName},");

            return span;
        }

        public static IHtmlContent Footer(this IBasicEmail email, SectionType sectionType)
        {
            var paragraph = new TagBuilder("div");
            var hr = new TagBuilder("hr");

            var p1 = new TagBuilder("p");
            var p2 = new TagBuilder("p");
            p2.MergeAttribute("style", "padding: 10px; display:block; border: 1px solid #ccc; background-color: #efefef; margin: 20px; font-weight:bold");

            paragraph.InnerHtml.AppendHtml(hr);
            paragraph.InnerHtml.AppendHtml(p1);
            paragraph.InnerHtml.AppendHtml(p2);

            p1.InnerHtml.Append(GetGenericFooterMessage(sectionType));
            p2.InnerHtml.Append(GetCommonMessage());

            return paragraph;
        }

        public static IHtmlContent Footer(this IBasicEmail email, SectionType sectionType, [NotNull] string organization)
        {
            var paragraph = new TagBuilder("div");
            var hr = new TagBuilder("hr");

            var p1 = new TagBuilder("p");
            var p2 = new TagBuilder("p");
            p2.MergeAttribute("style","padding: 10px; display:block; border: 1px solid #ccc; background-color: #efefef; margin: 20px; font-weight:bold");

            paragraph.InnerHtml.AppendHtml(hr);
            paragraph.InnerHtml.AppendHtml(p1);
            paragraph.InnerHtml.AppendHtml(p2);

            p1.InnerHtml.Append(GetGenericFooterMessage(sectionType, organization));
            p2.InnerHtml.Append(GetCommonMessage());

            return paragraph;
        }
        
        private static string GetGenericFooterMessage(SectionType role, [NotNull]  string organization)
        {
            var roleDescription = role.GetDescription();

            var article = roleDescription.ToLower().StartsWith("a") ? "an" : "a";

            return $"You are receiving this email because you are {article} {roleDescription} in {organization}";
        }

        private static string GetGenericFooterMessage(SectionType role)
        {
            var roleDescription = role.GetDescription();
            
            return $"You are receiving this email because you have an IdeaFortune account";
        }

        private static string GetCommonMessage()
        {
            return $"IdeaFortune: The Professional Services Platform";
        }
    }
}
