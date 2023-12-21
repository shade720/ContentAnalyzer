using Common.SharedDomain;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ContentAnalyzer.Frontend.Desktop.BusinessLogicLayer;

public class Reporter
{
    public void OpenReport(string path, IEnumerable<EvaluatedComment> evaluatedComments)
    {
        var report = Generate(evaluatedComments);
        File.WriteAllBytes(path, report);
    }

    private static byte[] Generate(IEnumerable<EvaluatedComment> evaluatedComments)
    {
        var package = new ExcelPackage();

        var sheet =
            package.Workbook.Worksheets
                .Add("Evaluation results");

        sheet.Cells[1, 1, 1, 9].LoadFromArrays(new[]
        { new object[]
        {
            "Text",
            "Category",
            "Probability",
            "Link",
            "Date",
            "Comment id",
            "Author id",
            "Post id",
            "Group id"
        } });

        var row = 2;
        const int column = 1;
        foreach (var item in evaluatedComments)
        {
            sheet.Cells[row, column].Value = item.RelatedComment.Text;
            sheet.Cells[row, column + 1].Value = item.EvaluateCategory;
            sheet.Cells[row, column + 2].Value = item.EvaluateProbability;

            var link = $"http://vk.com/wall{item.RelatedComment.GroupId}_{item.RelatedComment.PostId}?reply={item.RelatedComment.CommentId}";

            sheet.Cells[row, column + 3].Hyperlink = new Uri(link);
            sheet.Cells[row, column + 4].Value = item.RelatedComment.PostDate;

            sheet.Cells[row, column + 5].Value = item.RelatedComment.CommentId;
            sheet.Cells[row, column + 6].Value = item.RelatedComment.AuthorId;
            sheet.Cells[row, column + 7].Value = item.RelatedComment.PostId;
            sheet.Cells[row, column + 8].Value = item.RelatedComment.GroupId;
            sheet.Row(row).Height = 100;
            sheet.Row(row).Style.WrapText = true;
            row++;
        }

        sheet.Column(1).Width = 100;
        sheet.Column(1).Style.VerticalAlignment = ExcelVerticalAlignment.Top;
        sheet.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

        sheet.Column(3).Style.Numberformat.Format = "#0.00%";
        sheet.Column(5).Style.Numberformat.Format = "dd.MM.yyyy HH:mm";

        sheet.Cells[1, 2, row, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        sheet.Cells[1, 2, row, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        sheet.Cells[1, 2, row, 9].AutoFitColumns();
        sheet.Column(5).AutoFit(15);

        sheet.Column(6).Hidden = true;
        sheet.Column(7).Hidden = true;
        sheet.Column(8).Hidden = true;
        sheet.Column(9).Hidden = true;

        sheet.Cells[1, 1, 1, 8].Style.Font.Bold = true;

        return package.GetAsByteArray();
    }

}