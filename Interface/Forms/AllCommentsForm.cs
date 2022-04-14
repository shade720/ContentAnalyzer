using Common;

namespace Interface.Forms;

public partial class AllCommentsForm : Form
{
    private readonly MainWindow _parent;
    private int _lastIndex;
    private CancellationTokenSource _cancellationTokenSource;
    private int _rowDisplayed;

    public AllCommentsForm(MainWindow parent)
    {
        _parent = parent;
        InitializeComponent();
    }

    public async Task DisplayActualData()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _lastIndex = 0;
        AllCommentsDataGridView.Rows.Clear();
        await Task.Run(async () =>
        {
            while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                RefreshTable();
                await Task.Delay(5000, _cancellationTokenSource.Token);
            }
        }, _cancellationTokenSource.Token);
    }

    public void StopDisplayData()
    {
        _cancellationTokenSource.Cancel();
    }

    private void RefreshTable()
    {
        var list = DataCollectionService.DataCollectionService.GetAllComments(_lastIndex);
        UpdateControls(list);
        _lastIndex = AllCommentsDataGridView.Rows.Count;
    }

    private void UpdateControls(List<CommentData> list)
    {
        if (InvokeRequired)
        {
            Invoke(new Action<List<CommentData>>(UpdateControls), list);
            return;
        }
        foreach (var comment in list)
        {
            AllCommentsDataGridView.Rows.Add(
                comment.Id,
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