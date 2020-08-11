using IdeaFortune.Core.Contracts.ViewModels;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IdeaFortune.Mailer.TagHelpers
{
    public static partial class TableHelpers
    {
        public static IHtmlContent ContractDetails<T>(this T contract, decimal rate, string rateName)
            where T : ContractOutput, new()
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

            var projectNameHeader = new TagBuilder("th");
            projectNameHeader.MergeAttribute(
                "style",
                ColumnHeaderStyle);

            var projectManagerHeader = new TagBuilder("th");
            projectManagerHeader.MergeAttribute(
                "style",
                ColumnHeaderStyle);


            var maxHoursHeader = new TagBuilder("th");
            maxHoursHeader.MergeAttribute(
                "style",
                ColumnHeaderStyle);

            var contractorNameHeader = new TagBuilder("th");
            contractorNameHeader.MergeAttribute(
                "style",
                ColumnHeaderStyle);

            var streamNameHeader = new TagBuilder("th");
            streamNameHeader.MergeAttribute(
                "style",
                ColumnHeaderStyle);

            var earningPotentialHeader = new TagBuilder("th");
            earningPotentialHeader.MergeAttribute(
                "style",
                ColumnHeaderStyle);


            var projectNameCell = new TagBuilder("td");
            projectNameCell.MergeAttribute(
                "style",
                CellStyle);

            var projectManagerCell = new TagBuilder("td");
            projectManagerCell.MergeAttribute(
                "style",
                CellStyle);

            var weeklyMaxHoursCell = new TagBuilder("td");
            weeklyMaxHoursCell.MergeAttribute(
                "style",
                CellStyle);

            var streamCell = new TagBuilder("td");
            streamCell.MergeAttribute(
                "style",
                CellStyle);

            var contractorNameCell = new TagBuilder("td");
            contractorNameCell.MergeAttribute(
                "style",
                CellStyle);

            var earningPotentialCell = new TagBuilder("td");
            earningPotentialCell.MergeAttribute(
                "style",
                CellStyle);

            projectNameCell.InnerHtml.Append(contract.ProjectName);
            weeklyMaxHoursCell.InnerHtml.Append(contract.MaxWeeklyHours.ToString());
            streamCell.InnerHtml.Append(rate.ToString("C"));
            earningPotentialCell.InnerHtml.Append((rate * contract.MaxWeeklyHours).ToString("C"));
            contractorNameCell.InnerHtml.Append(contract.ContractorName);
            projectManagerCell.InnerHtml.Append($"{contract.ProjectManagerName}");

            projectNameHeader.InnerHtml.Append("Project");
            projectManagerHeader.InnerHtml.Append($"Project Manager [{contract.ProjectManagerOrganizationName}]");
            maxHoursHeader.InnerHtml.Append("Max Hours");
            streamNameHeader.InnerHtml.Append(rateName);
            earningPotentialHeader.InnerHtml.Append("Your Potential ($/wk)");
            contractorNameHeader.InnerHtml.Append($"Contractor [{contract.ProjectManagerOrganizationName}]");

            headers.InnerHtml.AppendHtml(projectNameHeader);
            headers.InnerHtml.AppendHtml(projectManagerHeader);
            headers.InnerHtml.AppendHtml(contractorNameHeader);
            headers.InnerHtml.AppendHtml(maxHoursHeader);
            headers.InnerHtml.AppendHtml(streamNameHeader);
            headers.InnerHtml.AppendHtml(earningPotentialHeader);

            cells.InnerHtml.AppendHtml(projectNameCell);
            cells.InnerHtml.AppendHtml(projectManagerCell);
            cells.InnerHtml.AppendHtml(contractorNameCell);
            cells.InnerHtml.AppendHtml(weeklyMaxHoursCell);
            cells.InnerHtml.AppendHtml(streamCell);
            cells.InnerHtml.AppendHtml(earningPotentialCell);

            table.InnerHtml.AppendHtml(headers);
            table.InnerHtml.AppendHtml(cells);

            return table;
        }
    }
}