using DevTool.Models.LogModel;

namespace DevTool.Forms;

public partial class LogViewerWindow : Form
{
    private IEnumerable<LogEntry> Logs;
    public LogViewerWindow(IEnumerable<LogEntry> logs)
    {
        InitializeComponent();
        Logs = logs;
    }

    private void FillLogDataGrid(IEnumerable<LogEntry> logs)
    {
        LogDataGrid.Rows.Clear();
        foreach (var log in logs)
        {
            LogDataGrid.Rows.Add(log.Date, log.Level, log.Message);
        }
        for (var i = 0; i < LogDataGrid.Rows.Count; i++)
        {
            if (LogDataGrid.Rows[i].Cells[1].EditedFormattedValue.ToString().Contains("Error") ||
                LogDataGrid.Rows[i].Cells[1].EditedFormattedValue.ToString().Contains("Fatal"))
                LogDataGrid.Rows[i].Cells[1].Style.BackColor = Color.Crimson;
        }
    }

    private void LogDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex == -1) return;
        MessageTextbox.Text = LogDataGrid.Rows[e.RowIndex].Cells[2].EditedFormattedValue.ToString();
    }

    private void LogLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillLogDataGrid(Logs.Where(x => x.Level.ToString() == LogLevel.Text || LogLevel.Text == "All"));
    }


    private void SearchButton_Click(object sender, EventArgs e)
    {
        FillLogDataGrid(Logs.Where(x => x.Message.Contains(SearchTextbox.Text)));
    }

    private void RefreshButton_Click(object sender, EventArgs e)
    {

    }

    private void LogViewerWindow_Load(object sender, EventArgs e)
    {
        LogLevel.SelectedIndex = 0;
    }
}