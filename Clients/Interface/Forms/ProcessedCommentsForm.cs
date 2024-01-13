using Common.SharedDomain;
using ContentAnalyzer.Frontend.Desktop.BusinessLogicLayer;
using System;

namespace ContentAnalyzer.Frontend.Desktop.Forms;

public partial class ProcessedCommentsForm : Form
{
    private readonly BackendClientFactory _backendClientFactory;
    private readonly Reporter _reporter;

    public ProcessedCommentsForm(BackendClientFactory backendClientFactory)
    {
        InitializeComponent();
        _backendClientFactory = backendClientFactory;
        _reporter = new Reporter();
    }

    private async void SelectedCommentsForm_Load(object sender, EventArgs e)
    {
        using var backendClient = _backendClientFactory.GetClient();
        if (backendClient is null)
        {
            MessageBox.Show(@"There is no connection to the services", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        try
        {
            var evaluatedComments = await backendClient.GetEvaluatedCommentsAsync(new CommentsQueryFilter());
            UpdateControls(evaluatedComments);
        }
        catch (Exception exception)
        {
            MessageBox.Show($"There is no connection to the services\r\n\n{exception.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void UpdateControls(IEnumerable<EvaluatedComment> list)
    {
        SelectedCommentsDataGridView.Rows.Clear();
        foreach (var comment in list)
        {
            SelectedCommentsDataGridView.Rows.Add(
                comment.RelatedComment.PostDate,
                comment.RelatedComment.Text,
                comment.EvaluateCategory,
                comment.EvaluateProbability,
                $"https://vk.com/wall{comment.RelatedComment.GroupId}_{comment.RelatedComment.PostId}?reply={comment.RelatedComment.CommentId}"
            );
        }
        DisplayedRowsLabel.Text = SelectedCommentsDataGridView.Rows.Count.ToString();
    }

    private CommentsQueryFilter GetCurrentFilter()
    {
        var fromDate = DateTime.UnixEpoch;
        var toDate = DateTime.UnixEpoch;
        if (TodayRadioButton.Checked)
            fromDate = DateTime.Today;
        if (Last3DaysRadioButton.Checked)
            fromDate = DateTime.Today.AddDays(-3);
        if (LastWeekRadioButton.Checked)
            fromDate = DateTime.Today.AddDays(-7);
        if (LastMonthRadioButton.Checked)
            fromDate = DateTime.Today.AddMonths(-1);
        if (SelectedDateRadioButton.Checked)
        {
            fromDate = DateTime.Parse(FromDate.Text);
            toDate = DateTime.Parse(ToDate.Text);
            if (fromDate.Ticks >= toDate.Ticks)
                MessageBox.Show(@"'To' date must be more than 'From' date");
        }

        var filter = new CommentsQueryFilter
        {
            AuthorId = !string.IsNullOrEmpty(AuthorFilterTextBox.Text) ? long.Parse(AuthorFilterTextBox.Text) : 0,
            PostId = !string.IsNullOrEmpty(PostFilterTextBox.Text) ? long.Parse(PostFilterTextBox.Text) : 0,
            GroupId = !string.IsNullOrEmpty(CommunityFilterTextBox.Text) ? long.Parse(CommunityFilterTextBox.Text) : 0,
            Text = !string.IsNullOrEmpty(TextFilterTextBox.Text) ? TextFilterTextBox.Text : string.Empty,
            Category = !string.IsNullOrEmpty(CategoryFilterTextBox.Text) ? CategoryFilterTextBox.Text : string.Empty,
            FromDate = fromDate,
            ToDate = toDate
        };
        return filter;
    }

    private async void RefreshButton_Click(object sender, EventArgs e)
    {

        using var backendClient = _backendClientFactory.GetClient();
        if (backendClient is null)
        {
            MessageBox.Show(@"There is no connection to the services", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        try
        {
            var evaluatedComments = await backendClient.GetEvaluatedCommentsAsync(GetCurrentFilter());
            UpdateControls(evaluatedComments);
        }
        catch (Exception exception)
        {
            MessageBox.Show($"There is no connection to the services\r\n\n{exception.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void OpenExcelReportButton_Click(object sender, EventArgs e)
    {
        using var backendClient = _backendClientFactory.GetClient();
        if (backendClient is null)
        {
            MessageBox.Show(@"There is no connection to the services", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        if (SaveReportDialog.ShowDialog() != DialogResult.OK)
            return;
        try
        {
            var evaluatedComments = await backendClient.GetEvaluatedCommentsAsync(GetCurrentFilter());
            _reporter.OpenReport(SaveReportDialog.FileName, evaluatedComments);
            MessageBox.Show(@"The report was created successfully!", @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception exception)
        {
            MessageBox.Show($"There is no connection to the services\r\n\n{exception.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void SelectedDateRadioButton_CheckedChanged(object sender, EventArgs e)
    {
        ToDate.Enabled = SelectedDateRadioButton.Checked;
        FromDate.Enabled = SelectedDateRadioButton.Checked;
    }
}