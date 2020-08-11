using IdeaFortune.Core.OrganizationPeople.ViewModels;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IdeaFortune.Mailer.TagHelpers
{
    public static partial class TableHelpers
    {
        public static IHtmlContent PersonDetails<T>(this T model)
            where T : OrganizationPersonOutput, new()
        {
            var table = new TagBuilder("table");
            table.Attributes["border"] = "1";
            table.MergeAttribute(
                "style",
                TableStyle);

            var headers = new TagBuilder("tr");
            headers.MergeAttribute(
                "style",
                RowHeaderStyle);

            var cells = new TagBuilder("tr");
            cells.MergeAttribute("style", RowCellsStyle);
            
            

            table.InnerHtml.AppendHtml(headers);
            table.InnerHtml.AppendHtml(cells);

            return table;
        }
    }
}