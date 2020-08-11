using IdeaFortune.Core.TimeEntries.ViewModels;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IdeaFortune.Mailer.TagHelpers
{
    public static partial class TableHelpers
    {
        public static IHtmlContent TimeEntryDetails<T>(this T entry, decimal stream, string streamName)
            where T : TimeEntryOutput, new()
        {
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

            var contractorHeader = new TagBuilder("th");
            contractorHeader.MergeAttribute("style", ColumnHeaderStyle);
            var contractorCell = new TagBuilder("td");
            contractorCell.MergeAttribute("style", CellStyle);

            var streamHeader = new TagBuilder("th");
            streamHeader.MergeAttribute("style", ColumnHeaderStyle);

            var streamCell = new TagBuilder("td");
            streamCell.MergeAttribute("style", CellStyle);

            headers.InnerHtml.AppendHtml(contractorHeader);
            headers.InnerHtml.AppendHtml(streamHeader);

            cells.InnerHtml.AppendHtml(contractorCell);
            cells.InnerHtml.AppendHtml(streamCell);

            streamCell.InnerHtml.Append(stream.ToString("C"));
            streamHeader.InnerHtml.Append($"{streamName}");

            contractorCell.InnerHtml.Append(entry.ContractorName);
            contractorHeader.InnerHtml.Append("Contractor");

            table.InnerHtml.AppendHtml(headers);
            table.InnerHtml.AppendHtml(cells);

            return table;
        }
    }
}