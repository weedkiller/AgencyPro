using IdeaFortune.Core.Candidates.ViewModels;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IdeaFortune.Mailer.TagHelpers
{
    public static partial class TableHelpers
    {
        public static IHtmlContent CandidateDetails<T>(this T candidate, decimal stream, string streamName, decimal bonus,
            string bonusName)
            where T : CandidateOutput, new()
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

            var bonusHeader = new TagBuilder("th");
            bonusHeader.MergeAttribute("style", ColumnHeaderStyle);

            var streamHeader = new TagBuilder("th");
            streamHeader.MergeAttribute("style", ColumnHeaderStyle);

            var contractorCell = new TagBuilder("td");
            contractorCell.MergeAttribute("style", CellStyle);

            var bonusCell = new TagBuilder("td");
            bonusCell.MergeAttribute("style", CellStyle);

            var streamCell = new TagBuilder("td");
            streamCell.MergeAttribute("style", CellStyle);

            headers.InnerHtml.AppendHtml(contractorHeader);
            headers.InnerHtml.AppendHtml(bonusHeader);
            headers.InnerHtml.AppendHtml(streamHeader);

            cells.InnerHtml.AppendHtml(contractorCell);
            cells.InnerHtml.AppendHtml(bonusCell);
            cells.InnerHtml.AppendHtml(streamCell);

            contractorCell.InnerHtml.Append(candidate.FirstName + " " + candidate.LastName + $" [{candidate.ProviderOrganizationName}]");
            bonusCell.InnerHtml.Append(bonus.ToString("C"));
            bonusHeader.InnerHtml.Append(bonusName);
            contractorHeader.InnerHtml.Append("Candidate");
            streamCell.InnerHtml.Append(stream.ToString("C"));
            streamHeader.InnerHtml.Append(streamName);

            table.InnerHtml.AppendHtml(headers);
            table.InnerHtml.AppendHtml(cells);

            return table;
        }
    }
}