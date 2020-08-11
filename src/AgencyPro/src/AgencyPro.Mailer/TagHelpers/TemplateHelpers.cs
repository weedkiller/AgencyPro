using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IdeaFortune.Mailer.TagHelpers
{
    public static class TemplateHelpers
    {
        public static IHtmlContent SingleParagraph(string message)
        {
            var root = new TagBuilder("div");
            var p1 = new TagBuilder("p");

            p1.InnerHtml.Append(message);

            root.InnerHtml.AppendHtml(p1);

            return root;
        }

        public static IHtmlContent TwoParagraph(string message, string link)
        {
            var flowLink = new TagBuilder("a");
            flowLink.Attributes["href"] = link;
            flowLink.InnerHtml.Append("View in Flow");

            var root = new TagBuilder("div");
            var p1 = new TagBuilder("p");
            var p2 = new TagBuilder("p");

            p1.InnerHtml.Append(message);
            p2.InnerHtml.AppendHtml(flowLink);

            root.InnerHtml.AppendHtml(p1);
            root.InnerHtml.AppendHtml(p2);

            return root;
        }

        public static IHtmlContent ThreeParagraph(string message, string link, IHtmlContent html)
        {
            var flowLink = new TagBuilder("a");
            flowLink.Attributes["href"] = link;
            flowLink.InnerHtml.Append("View in Flow");

            var root = new TagBuilder("div");
            var p1 = new TagBuilder("p");
            var p2 = new TagBuilder("p");

            p1.InnerHtml.Append(message);
            p2.InnerHtml.AppendHtml(flowLink);

            root.InnerHtml.AppendHtml(p1);
            root.InnerHtml.AppendHtml(html);
            root.InnerHtml.AppendHtml(p2);

            return root;
        }
    }
}