using IdeaFortune.Core.Stories.ViewModels;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IdeaFortune.Mailer.TagHelpers
{
    public static partial class TableHelpers
    {
        public static IHtmlContent StoryDetails<T>(this T story)
            where T : StoryOutput, new()
        {
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

            var projectNameHeader = new TagBuilder("th");
            projectNameHeader.MergeAttribute(
                "style",
                ColumnHeaderStyle);

            var storyTitleHeader = new TagBuilder("th");
            storyTitleHeader.MergeAttribute(
                "style",
                ColumnHeaderStyle);
            var storyPointsHeader = new TagBuilder("th");
            storyTitleHeader.MergeAttribute(
                "style",
                ColumnHeaderStyle);

            var projectNameCell = new TagBuilder("td");
            projectNameCell.MergeAttribute(
                "style",
                CellStyle);

            var storyTitleCell = new TagBuilder("td");
            storyTitleCell.MergeAttribute(
                "style",
                CellStyle);

            var storyPointsCell = new TagBuilder("td");
            storyPointsCell.MergeAttribute(
                "style",
                CellStyle);

            projectNameHeader.InnerHtml.Append("Project");
            storyTitleHeader.InnerHtml.Append("Story Title");

            projectNameCell.InnerHtml.Append(story.ProjectName);
            storyTitleCell.InnerHtml.Append(story.Title);
            headers.InnerHtml.AppendHtml(projectNameHeader);
            headers.InnerHtml.AppendHtml(storyTitleHeader);

            cells.InnerHtml.AppendHtml(projectNameCell);
            cells.InnerHtml.AppendHtml(storyTitleCell);

            if (story.StoryPoints.HasValue)
            {
                storyPointsHeader.InnerHtml.Append("Story Points");
                storyPointsCell.InnerHtml.Append(story.StoryPoints.Value.ToString());
                headers.InnerHtml.AppendHtml(storyPointsHeader);
                cells.InnerHtml.AppendHtml(storyPointsCell);
            }

            table.InnerHtml.AppendHtml(headers);
            table.InnerHtml.AppendHtml(cells);

            return table;
        }
    }
}