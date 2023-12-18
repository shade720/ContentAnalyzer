using Common.SharedDomain;

namespace ContentAnalyzer.Frontend.Desktop.Forms;

public partial class SelectedCommentsForm : Form
{
    private readonly MainWindow _parent;
    private readonly Client _client;
    private int _lastIndex;
    private CancellationTokenSource _cancellationTokenSource;
    private int _rowDisplayed;

    public SelectedCommentsForm(MainWindow parent, Client client)
    {
        _parent = parent;
        _client = client;
        InitializeComponent();
    }

    public async Task DisplayActualData()
    {
        _lastIndex = 0;
        _cancellationTokenSource = new CancellationTokenSource();
        SelectedCommentsDataGridView.Rows.Clear();
        var progress = new Progress<List<EvaluatedComment>>(UpdateControls);
        await Task.Run(async () => await GettingResultsLoop(progress), _cancellationTokenSource.Token);
    }
    public void StopDisplayData()
    {
        _cancellationTokenSource.Cancel();
    }

    private async Task GettingResultsLoop(IProgress<List<EvaluatedComment>> progress)
    {
        while (!_cancellationTokenSource.Token.IsCancellationRequested)
        {
            await Task.Delay(5000, _cancellationTokenSource.Token);
            var list = _client.GetEvaluateResults(_lastIndex);
            if (list.Count == 0) continue;
            progress.Report(list);
            _lastIndex = list[^1].Id;
        }
    }

    private void UpdateControls(List<EvaluatedComment> list)
    {
        foreach (var comment in list)
        {
            SelectedCommentsDataGridView.Rows.Add(
                comment.RelatedComment.CommentId,
                comment.RelatedComment.PostDate,
                comment.RelatedComment.Text,
                comment.EvaluateCategory,
                comment.EvaluateProbability
            );
        }
        DisplayedRowsLabel.Text = _rowDisplayed == 0 ? SelectedCommentsDataGridView.Rows.Count.ToString() : _rowDisplayed.ToString();
        _parent.SelectedCommentsFoundLabel.Text = SelectedCommentsDataGridView.Rows.Count.ToString();
    }

    private void SearchTextBox_TextChanged(object sender, EventArgs e)
    {
        ShowAll();
        if (SearchTextBox.Text == "") return;
        HideByColumn(SearchComboBox.SelectedIndex);
        DisplayedRowsLabel.Text = _rowDisplayed.ToString();
    }

    private void ShowAll()
    {
        _rowDisplayed = 0;
        foreach (DataGridViewRow row in SelectedCommentsDataGridView.Rows)
        {
            row.Visible = true;
        }
    }

    private void HideByColumn(int columnNum)
    {
        foreach (DataGridViewRow row in SelectedCommentsDataGridView.Rows)
        {
            if (!row.Cells[columnNum].Value.ToString().ToLower().Contains(SearchTextBox.Text.ToLower())) row.Visible = false;
            else _rowDisplayed++;
        }
    }

    private void SelectedCommentsForm_Load(object sender, EventArgs e)
    {
        SearchComboBox.SelectedIndex = 0;
    }
}