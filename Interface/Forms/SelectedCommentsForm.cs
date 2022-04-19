using Common.EntityFramework;

namespace Interface.Forms;

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
        var list = _client.GetEvaluateResults(_lastIndex);
        if(list.Count == 0) return;
        UpdateControls(list);
        _lastIndex = list[^1].Id;
    }

    private void UpdateControls(List<EvaluateResult> list)
    {
        if (InvokeRequired)
        {
            Invoke(new Action<List<EvaluateResult>>(UpdateControls), list);
            return;
        }
        foreach (var comment in list)
        {
            SelectedCommentsDataGridView.Rows.Add(
                comment.CommentData.CommentId,
                comment.CommentData.PostDate,
                comment.CommentData.Text,
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