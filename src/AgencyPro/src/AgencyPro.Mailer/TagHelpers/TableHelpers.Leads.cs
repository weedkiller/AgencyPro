using IdeaFortune.Core.Leads.ViewModels;
using IdeaFortune.Core.Metadata;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IdeaFortune.Mailer.TagHelpers
{
    public static partial class TableHelpers
    {
        public static IHtmlContent LeadDetails<T>(this T lead, decimal stream, string streamName, decimal bonus,
            string bonusName)
            where T : LeadOutput, new()
        {
            var role = FlowExtensions.GetRole(typeof(T));
            
            var table = new TagBuilder("table");
            var cells = new TagBuilder("tr");
            var headers = new TagBuilder("tr");

            table.Attributes["border"] = "1";
            table.MergeAttribute(
                "style",
                TableStyle);

            headers.MergeAttribute(
                "style",
                RowHeaderStyle);

            cells.MergeAttribute(
                "style",
                RowCellsStyle);


            var customerHeader = new TagBuilder("th");
            customerHeader.MergeAttribute("style", ColumnHeaderStyle);

            var customerCell = new TagBuilder("td");
            customerCell.MergeAttribute("style", CellStyle);

            var bonusHeader = new TagBuilder("th");
            bonusHeader.MergeAttribute("style", ColumnHeaderStyle);

            var streamHeader = new TagBuilder("th");
            streamHeader.MergeAttribute("style", ColumnHeaderStyle);

            var marketerHeader = new TagBuilder("th");
            marketerHeader.MergeAttribute("style", ColumnHeaderStyle);

            var bonusCell = new TagBuilder("td");
            bonusCell.MergeAttribute("style", CellStyle);

            var streamCell = new TagBuilder("td");
            streamCell.MergeAttribute("style", CellStyle);

            var marketerCell = new TagBuilder("td");
            marketerCell.MergeAttribute("style", CellStyle);

            headers.InnerHtml.AppendHtml(customerHeader);
            headers.InnerHtml.AppendHtml(bonusHeader);
            headers.InnerHtml.AppendHtml(streamHeader);
            headers.InnerHtml.AppendHtml(marketerHeader);

            cells.InnerHtml.AppendHtml(customerCell);
            cells.InnerHtml.AppendHtml(bonusCell);
            cells.InnerHtml.AppendHtml(streamCell);
            cells.InnerHtml.AppendHtml(marketerCell);

            bonusCell.InnerHtml.Append(bonus.ToString("C"));
            marketerCell.InnerHtml.Append(lead.MarketerName);
            bonusHeader.InnerHtml.Append(bonusName);
            streamCell.InnerHtml.Append(stream.ToString("C"));
            marketerHeader.InnerHtml.Append($"Marketer [{lead.MarketerOrganizationName}]");
            streamHeader.InnerHtml.Append(streamName);
            customerHeader.InnerHtml.Append("Lead Name");
            customerCell.InnerHtml.Append($"{lead.FirstName} {lead.LastName} ({lead.OrganizationName})");

            table.InnerHtml.AppendHtml(headers);
            table.InnerHtml.AppendHtml(cells);

            return table;
        }
    }
}