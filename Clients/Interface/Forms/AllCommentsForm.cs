using Common.EntityFramework;

namespace Interface.Forms;

public partial class AllCommentsForm : Form
{
    private readonly MainWindow _parent;
    private readonly Client _client;
    private long _lastIndex;
    private CancellationTokenSource _cancellationTokenSource;
    private int _rowDisplayed;

    public AllCommentsForm(MainWindow parent, Client client)
    {
        _parent = parent;
        _client = client;
        InitializeComponent();
    }

    public async Task DisplayActualData()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _lastIndex = 0;
        AllCommentsDataGridView.Rows.Clear();
        var progress = new Progress<List<Comment>>(UpdateControls);
        await Task.Run(async () => await GettingResultsLoop(progress), _cancellationTokenSource.Token);
    }

    public void StopDisplayData()
    {
        _cancellationTokenSource.Cancel();
    }

    private async Task GettingResultsLoop(IProgress<List<Comment>> progress)
    {
        while (!_cancellationTokenSource.Token.IsCancellationRequested)
        {
            await Task.Delay(5000, _cancellationTokenSource.Token);
            var list = _client.GetComments((int)_lastIndex);
            if (list.Count == 0) continue;
            progress.Report(list);
            _lastIndex = list[^1].Id;
        }
    }

    private void UpdateControls(List<Comment> list)
    {
        foreach (var comment in list)
        {
            AllCommentsDataGridView.Rows.Add(
                comment.CommentId,
                comment.PostId,
                comment.GroupId,
                comment.AuthorId,
                comment.PostDate,
                comment.Text
            );
        }
        DisplayedRowsLabel.Text = _rowDisplayed == 0 ? AllCommentsDataGridView.Rows.Count.ToString() : _rowDisplayed.ToString();
        _parent.CommentsFoundLabel.Text = AllCommentsDataGridView.Rows.Count.ToString();
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
        foreach (DataGridViewRow row in AllCommentsDataGridView.Rows)
        {
             row.Visible = true;
        }
    }

    private void HideByColumn(int columnNum)
    {
        foreach (DataGridViewRow row in AllCommentsDataGridView.Rows)
        {
            if (!row.Cells[columnNum].Value.ToString().ToLower().Contains(SearchTextBox.Text.ToLower()))
                row.Visible = false;
            else _rowDisplayed++;
        }
    }

    private void AllCommentsForm_Load(object sender, EventArgs e)
    {
        SearchComboBox.SelectedIndex = 0;
    }
}