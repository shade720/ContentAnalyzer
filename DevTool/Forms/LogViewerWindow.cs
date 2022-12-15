using DevTool.Models;
using System.Collections.Generic;

namespace DevTool.Forms;

public partial class LogViewerWindow : Form
{
    private IEnumerable<LogInfo> Logs;
    public LogViewerWindow(IEnumerable<LogInfo> logs)
    {
        InitializeComponent();
        
        Logs = logs;
    }

    private void FillLogDataGrid(IEnumerable<LogInfo> logs)
    {
        LogDataGrid.Rows.Clear();
        foreach (var log in logs)
        {
            LogDataGrid.Rows.Add(log.Date, log.Level, log.Message);
        }
    }

    private void LogDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        MessageTextbox.Text = LogDataGrid.Rows[e.RowIndex].Cells[2].EditedFormattedValue.ToString();
    }

    private void LogLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillLogDataGrid(Logs.Where(x => x.Level.ToString() == LogLevel.Text || LogLevel.Text == "All"));
    }
}