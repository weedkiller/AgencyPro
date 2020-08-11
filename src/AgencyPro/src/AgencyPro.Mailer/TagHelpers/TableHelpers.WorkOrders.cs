using IdeaFortune.Core.Metadata;
using IdeaFortune.Core.Orders.ViewModels;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IdeaFortune.Mailer.TagHelpers
{
    public static partial class TableHelpers
    {
        public static IHtmlContent WorkOrderDetails<T>(this T model)
            where T : WorkOrderOutput, new()
        {
            int number = model.ProviderNumber;
            var role = FlowExtensions.GetRole(typeof(T));
            switch (role)
            {
                case FlowRoleToken.Customer:
                    number = model.BuyerNumber;
                    break;
            }

            var table = new TagBuilder("table");
            table.Attributes["border"] = "1";
            table.MergeAttribute(
                "style",
                $"border-collapse: collapse;border: 1px solid black");

            var headers = new TagBuilder("tr");
            headers.MergeAttribute(
                "style",
                RowHeaderStyle);

            var cells = new TagBuilder("tr");
            cells.MergeAttribute("style", RowCellsStyle);

            
            var accountManagerHeader = new TagBuilder("th");
            accountManagerHeader.MergeAttribute(
                "style",
                ColumnHeaderStyle);

            var numberHeader = new TagBuilder("th");
            accountManagerHeader.MergeAttribute(
                "style",
                ColumnHeaderStyle);

            var customerHeader = new TagBuilder("th");
            customerHeader.MergeAttribute(
                "style",
                ColumnHeaderStyle);


            var accountManagerCell = new TagBuilder("td");
            accountManagerCell.MergeAttribute(
                "style",
                CellStyle);

            var numberCell = new TagBuilder("td");
            accountManagerCell.MergeAttribute(
                "style",
                CellStyle);


            var customerCell = new TagBuilder("td");
            customerCell.MergeAttribute(
                "style",
                CellStyle);

            accountManagerCell.InnerHtml.Append($"{model.AccountManagerName}");
            customerCell.InnerHtml.Append($"{model.CustomerName}");
            numberCell.InnerHtml.Append($"{number}");

            accountManagerHeader.InnerHtml.Append($"AM [{model.AccountManagerOrganizationName}]");
            customerHeader.InnerHtml.Append($"CU [{model.CustomerOrganizationName}]");
            numberHeader.InnerHtml.Append($"Number");

            headers.InnerHtml.AppendHtml(numberHeader);
            headers.InnerHtml.AppendHtml(accountManagerHeader);
            headers.InnerHtml.AppendHtml(customerHeader);

            cells.InnerHtml.AppendHtml(numberCell);
            cells.InnerHtml.AppendHtml(accountManagerCell);
            cells.InnerHtml.AppendHtml(customerCell);

            table.InnerHtml.AppendHtml(headers);
            table.InnerHtml.AppendHtml(cells);

            return table;
        }
    }
}