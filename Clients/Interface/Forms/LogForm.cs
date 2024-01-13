using ContentAnalyzer.Frontend.Desktop.BusinessLogicLayer;
using ContentAnalyzer.Frontend.Desktop.Models;

namespace ContentAnalyzer.Frontend.Desktop.Forms;

public partial class LogForm : Form
{
    private readonly MainWindow _parent;
    private readonly BackendClientFactory _backendClientFactory;
    private List<LogInfo> _logs = new();

    public LogForm(MainWindow parent, BackendClientFactory backendClientFactory)
    {
        InitializeComponent();
        _parent = parent;
        _backendClientFactory = backendClientFactory;
    }

    private async void LogViewerWindow_Load(object sender, EventArgs e)
    {
        SourceComboBox.SelectedIndex = 0;
        using var backendClient = _backendClientFactory.GetClient();
        if (backendClient is null)
        {
            MessageBox.Show(@"There is no connection to the services", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        try
        {
            var dataCollectionLogFile = await backendClient.GetDataCollectionServiceLogsAsync(DateTime.Now);
            var dataAnalysisLogFile = await backendClient.GetDataAnalysisServiceLogsAsync(DateTime.Now);
            LogDataGrid.Rows.Clear();
            var backendState = new BackendState();
            foreach (var logInfo in LogFileParser(dataCollectionLogFile))
            {
                if (logInfo.Level == Models.LogLevel.Error || logInfo.Level == Models.LogLevel.Fatal) backendState.ErrorsCount++;
                else if (logInfo.Level == Models.LogLevel.Warning) backendState.WarningsCount++;
                else if (logInfo.Message.Contains("stopped")) backendState.CollectionServiceState = ServiceState.Down;
                else if (logInfo.Message.Contains("started")) backendState.CollectionServiceState = ServiceState.Up;
                LogDataGrid.Rows.Add(logInfo.Date, logInfo.Level, logInfo.Message);
                _logs.Add(logInfo);
            }
            foreach (var logInfo in LogFileParser(dataAnalysisLogFile))
            {
                if (logInfo.Level == Models.LogLevel.Error || logInfo.Level == Models.LogLevel.Fatal) backendState.ErrorsCount++;
                else if (logInfo.Level == Models.LogLevel.Warning) backendState.WarningsCount++;
                else if (logInfo.Message.Contains("stopped")) backendState.AnalysisServiceState = ServiceState.Down;
                else if (logInfo.Message.Contains("started")) backendState.AnalysisServiceState = ServiceState.Up;
                else if (logInfo.Message.Contains("evaluated")) backendState.ProcessedComments = int.Parse(logInfo.Message.Trim().Split(" ")[0]);
                LogDataGrid.Rows.Add(logInfo.Date, logInfo.Level, logInfo.Message);
                _logs.Add(logInfo);
            }
            for (var i = 0; i < LogDataGrid.Rows.Count; i++)
            {
                if (LogDataGrid.Rows[i].Cells[1].EditedFormattedValue.ToString().Contains("Error") ||
                    LogDataGrid.Rows[i].Cells[1].EditedFormattedValue.ToString().Contains("Fatal"))
                    LogDataGrid.Rows[i].Cells[1].Style.BackColor = Color.Red;
            }

            _parent.SetBackendState(backendState);
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        LogLevel.SelectedIndex = 0;
    }

    private void FillLogsDataGrid(IEnumerable<LogInfo> logs)
    {
        LogDataGrid.Rows.Clear();
        foreach (var logInfo in logs)
        {
            LogDataGrid.Rows.Add(logInfo.Date, logInfo.Level, logInfo.Message);
        }
        for (var i = 0; i < LogDataGrid.Rows.Count; i++)
        {
            if (LogDataGrid.Rows[i].Cells[1].EditedFormattedValue.ToString().Contains("Error") ||
                LogDataGrid.Rows[i].Cells[1].EditedFormattedValue.ToString().Contains("Fatal"))
                LogDataGrid.Rows[i].Cells[1].Style.BackColor = Color.Red;
        }
    }

    private void LogDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex == -1) return;
        MessageTextbox.Text = LogDataGrid.Rows[e.RowIndex].Cells[2].EditedFormattedValue.ToString();
    }

    private void LogLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillLogsDataGrid(_logs.Where(x => x.Level.ToString() == LogLevel.Text || LogLevel.Text == "All"));
    }


    private void SearchButton_Click(object sender, EventArgs e)
    {
        FillLogsDataGrid(_logs.Where(x => x.Message.Contains(SearchTextbox.Text)));
    }

    private async void RefreshButton_Click(object sender, EventArgs e)
    {
        try
        {
            _logs = (await GetLogsByFilter(SourceComboBox.Text, DateTime.Parse(FromDate.Text), DateTime.Parse(ToDate.Text))).ToList();
            FillLogsDataGrid(_logs);
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            _logs = new List<LogInfo>();
        }
    }

    private async Task<IEnumerable<LogInfo>> GetLogsByFilter(string source, DateTime fromDate, DateTime toDate)
    {
        var result = new List<LogInfo>();
        using var backendClient = _backendClientFactory.GetClient();
        if (backendClient is null)
        {
            MessageBox.Show(@"There is no connection to the services", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return result;
        }
        if (fromDate.Ticks > toDate.Ticks)
        {
            MessageBox.Show(@"'To' date must be more than 'From' date");
            return result;
        }

        foreach (var day in GetDateTimeRange(fromDate, toDate))
        {
            result.AddRange(source switch
            {
                "Все" => LogFileParser(await backendClient.GetDataCollectionServiceLogsAsync(day)).Concat(LogFileParser(await backendClient.GetDataAnalysisServiceLogsAsync(day))),
                "Сервис сбора" => LogFileParser(await backendClient.GetDataCollectionServiceLogsAsync(day)),
                "Сервис анализа" => LogFileParser(await backendClient.GetDataAnalysisServiceLogsAsync(day)),
                _ => result
            });
        }
        return result;
    }

    private static IEnumerable<LogInfo> LogFileParser(string logFile)
    {
        if (string.IsNullOrEmpty(logFile)) yield break;
        var logRecords = logFile.Split("`~");
        foreach (var log in logRecords)
        {
            if (string.IsNullOrEmpty(log) || log.Length < 10)
                continue;
            var date = log[..(log.IndexOf('+') - 2)];
            var level = log[log.IndexOf('[')..(log.IndexOf(']') + 1)];
            var logMessage = log[(log.IndexOf(']') + 1)..].Replace(@"\n", "");
            yield return new LogInfo
            {
                Date = DateTime.Parse(date),
                Level = level switch
                {
                    "[Fatal]" => Models.LogLevel.Fatal,
                    "[Error]" => Models.LogLevel.Error,
                    "[Warning]" => Models.LogLevel.Warning,
                    "[Information]" => Models.LogLevel.Information,
                    _ => Models.LogLevel.Information
                },
                Message = logMessage
            };
        }
    }

    private static IEnumerable<DateTime> GetDateTimeRange(DateTime fromDate, DateTime toDate)
    {
        var result = new List<DateTime> { fromDate };
        var dayToAdd = fromDate;
        while (dayToAdd < toDate)
        {
            dayToAdd = dayToAdd.AddDays(1);
            result.Add(dayToAdd);
        }
        return result;
    }
}