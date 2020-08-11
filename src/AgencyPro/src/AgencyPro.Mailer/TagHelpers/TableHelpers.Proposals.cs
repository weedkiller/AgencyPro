using IdeaFortune.Core.Proposals.ViewModels;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IdeaFortune.Mailer.TagHelpers
{
    public static partial class TableHelpers
    {
        public static IHtmlContent ProposalDetails<T>(this T model)
            where T : FixedPriceProposalOutput, new()
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

            var accountManagerHeader = new TagBuilder("th");
            accountManagerHeader.MergeAttribute(
                "style",
                ColumnHeaderStyle);

            var customerHeader = new TagBuilder("th");
            customerHeader.MergeAttribute(
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

            var accountManagerCell = new TagBuilder("td");
            accountManagerCell.MergeAttribute(
                "style",
                CellStyle);


            var customerCell = new TagBuilder("td");
            customerCell.MergeAttribute(
                "style",
                CellStyle);

            projectNameCell.InnerHtml.Append(model.ProjectName);
            projectManagerCell.InnerHtml.Append($"{model.ProjectManagerName}");
            accountManagerCell.InnerHtml.Append($"{model.AccountManagerName}");
            customerCell.InnerHtml.Append($"{model.CustomerName}");

            projectNameHeader.InnerHtml.Append("Project");
            projectManagerHeader.InnerHtml.Append($"Project Manager [{model.ProjectManagerOrganizationName}]");
            accountManagerHeader.InnerHtml.Append($"Account Manager [{model.AccountManagerOrganizationName}]");
            customerHeader.InnerHtml.Append($"Customer [{model.CustomerOrganizationName}]");

            headers.InnerHtml.AppendHtml(projectNameHeader);
            headers.InnerHtml.AppendHtml(projectManagerHeader);
            headers.InnerHtml.AppendHtml(accountManagerHeader);
            headers.InnerHtml.AppendHtml(customerHeader);

            cells.InnerHtml.AppendHtml(projectNameCell);
            cells.InnerHtml.AppendHtml(projectManagerCell);
            cells.InnerHtml.AppendHtml(accountManagerCell);
            cells.InnerHtml.AppendHtml(customerCell);

            table.InnerHtml.AppendHtml(headers);
            table.InnerHtml.AppendHtml(cells);

            return table;
        }
    }
}