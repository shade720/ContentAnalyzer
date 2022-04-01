using Common;

namespace Interface.Forms;

public partial class AllCommentsForm : Form
{
    private readonly MainWindow _parent;
    private int _lastIndex;
    private CancellationTokenSource _cancellationTokenSource;

    public AllCommentsForm(MainWindow parent)
    {
        _parent = parent;
        InitializeComponent();
    }

    public async Task DisplayActualData()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _lastIndex = 0;
        await Task.Run(() =>
        {
            while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                RefreshTable();
                Thread.Sleep(5000);
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

    private void UpdateControls(List<ICommentData> list)
    {
        if (InvokeRequired)
        {
            Invoke(new Action<List<ICommentData>>(UpdateControls), list);
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
        _parent.CommentsFoundLabel.Text = AllCommentsDataGridView.Rows.Count.ToString();
    }

    private void SearchTextBox_TextChanged(object sender, EventArgs e)
    {
        if (SearchTextBox.Text == "") return;
        if (SearchComboBox.SelectedIndex is -1)
        {
            MessageBox.Show("Select Select a search category");
            return;
        }
        ShowAll();
        HideByColumn(SearchComboBox.SelectedIndex);
    }

    private void ShowAll()
    {
        foreach (DataGridViewRow row in AllCommentsDataGridView.Rows)
        {
             row.Visible = true;
        }
        DisplayedRowsLabel.Text = AllCommentsDataGridView.Rows.Count.ToString();
    }

    private void HideByColumn(int columnNum)
    {
        var displayedRowCount = 0;
        foreach (DataGridViewRow row in AllCommentsDataGridView.Rows)
        {
            if (!row.Cells[columnNum].Value.ToString().ToLower().Contains(SearchTextBox.Text.ToLower())) row.Visible = false;
            else displayedRowCount++;
        }
        DisplayedRowsLabel.Text = displayedRowCount.ToString();
    }
}