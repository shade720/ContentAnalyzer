using Common;

namespace Interface.Forms;

public partial class SelectedCommentsForm : Form
{
    private readonly MainWindow _parent;
    private int _lastIndex;
    private CancellationTokenSource _cancellationTokenSource;

    public SelectedCommentsForm(MainWindow parent)
    {
        _parent = parent;
        InitializeComponent();
    }

    public async Task DisplayActualData()
    {
        _lastIndex = 0;
        _cancellationTokenSource = new CancellationTokenSource();
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
        var list = DataAnalysisService.DataAnalysisService.GetAllComments(_lastIndex);
        UpdateControls(list);
        _lastIndex = SelectedCommentsDataGridView.Rows.Count;
    }

    private void UpdateControls(List<IEvaluateResult> list)
    {
        if (InvokeRequired)
        {
            Invoke(new Action<List<IEvaluateResult>>(UpdateControls), list);
            return;
        }
        foreach (var comment in list)
        {
            SelectedCommentsDataGridView.Rows.Add(
                comment.CommentData.Id,
                comment.CommentData.PostDate,
                comment.CommentData.Text,
                comment.EvaluateCategory,
                comment.EvaluateProbability
            );
        }
        _parent.SelectedCommentsFoundLabel.Text = SelectedCommentsDataGridView.Rows.Count.ToString();
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
        foreach (DataGridViewRow row in SelectedCommentsDataGridView.Rows)
        {
            row.Visible = true;
        }
        DisplayedRowsLabel.Text = SelectedCommentsDataGridView.Rows.Count.ToString();
    }

    private void HideByColumn(int columnNum)
    {
        var displayedRowCount = 0;
        foreach (DataGridViewRow row in SelectedCommentsDataGridView.Rows)
        {
            if (!row.Cells[columnNum].Value.ToString().ToLower().Contains(SearchTextBox.Text.ToLower())) row.Visible = false;
            else displayedRowCount++;
        }
        DisplayedRowsLabel.Text = displayedRowCount.ToString();
    }
}