using DevTool.Models.LogModel;
using DevTool.Models.ServiceInfoModel;

namespace DevTool.BLL
{
    public class StateProvider
    {
        private readonly string _logsFolder;

        private ServiceInfo? _cachedServiceInfo;
        private DateTime _lastUpdateTime;

        public StateProvider(string logsFolder)
        {
            _logsFolder = logsFolder;
        }

        public ServiceInfo GetServiceInfoFromLogFile()
        {
            if (!Directory.Exists(_logsFolder) || Directory.GetFiles(_logsFolder).Length == 0)
                return new ServiceInfo();

            var logEntries = _cachedServiceInfo is null
                ? GetAllLogEntriesFromFolder(_logsFolder)
                : GetLastLogEntries(_logsFolder, _lastUpdateTime);

            _cachedServiceInfo = UpdateServiceInfo(_cachedServiceInfo ?? new ServiceInfo(), logEntries);

            _lastUpdateTime = DateTime.Now;

            return _cachedServiceInfo;
        }

        private static ServiceInfo UpdateServiceInfo(ServiceInfo updatedServiceInfo, IEnumerable<LogEntry> logEntries)
        {
            updatedServiceInfo.ErrorsCount = 0;
            updatedServiceInfo.WarningsCount = 0;
            foreach (var logInfo in logEntries)
            {
                if (logInfo.Level == LogLevel.Error) updatedServiceInfo.ErrorsCount++;
                else if (logInfo.Level == LogLevel.Warning) updatedServiceInfo.WarningsCount++;
                else if (logInfo.Message.Contains("stopped")) updatedServiceInfo.State = State.Down;
                else if (logInfo.Message.Contains("started")) updatedServiceInfo.State = State.Up;
                else if (logInfo.Message.Contains("collected")) updatedServiceInfo.CollectedCommentsCount = int.Parse(logInfo.Message.Trim().Split(" ")[0]);
                else if (logInfo.Message.Contains("evaluated")) updatedServiceInfo.EvaluatedCommentsCount = int.Parse(logInfo.Message.Trim().Split(" ")[0]);
                else if (logInfo.Message.Contains("Uptime"))
                {
                    updatedServiceInfo.Uptime = TimeSpan.Parse(logInfo.Message.Trim().Replace('"', ' ').Split(" ")[1]);
                    if (logInfo.Message.Contains("00:00:00")) updatedServiceInfo.State = State.Down;
                }
            }
            return updatedServiceInfo;
        }

        private static IEnumerable<LogEntry> GetAllLogEntriesFromFolder(string folderPath)
        {
            foreach (var file in Directory.GetFiles(folderPath))
            {
                using var sr = new StreamReader(file);
                while (!sr.EndOfStream)
                {
                    var logEntry = sr.ReadLine();
                    if (string.IsNullOrEmpty(logEntry))
                        continue;

                    var date = logEntry[..(logEntry.IndexOf('+') - 2)];
                    var level = logEntry[logEntry.IndexOf('[')..(logEntry.IndexOf(']') + 1)];
                    var logMessage = logEntry[(logEntry.IndexOf(']') + 1)..];

                    yield return new LogEntry
                    {
                        Date = DateTime.Parse(date),
                        Level = level switch
                        {
                            "[Fatal]" => LogLevel.Fatal,
                            "[Error]" => LogLevel.Error,
                            "[Warning]" => LogLevel.Warning,
                            "[Information]" => LogLevel.Information,
                            _ => LogLevel.Information
                        },
                        Message = logMessage
                    };
                }
            }
        }

        private static IEnumerable<LogEntry> GetLastLogEntries(string folderPath, DateTime lastUpdateTime)
        {
            var logFile = new DirectoryInfo(folderPath)
                .GetFileSystemInfos()
                .OrderBy(fi => fi.CreationTime)
                .First()
                .FullName;

            foreach (var logEntryStr in new ReverseLineReader(logFile))
            {
                if (string.IsNullOrEmpty(logEntryStr))
                    continue;

                var date = logEntryStr[..(logEntryStr.IndexOf('+') - 2)];
                var parsedDate = DateTime.Parse(date);
                if (parsedDate < lastUpdateTime)
                    yield break;

                var level = logEntryStr[logEntryStr.IndexOf('[')..(logEntryStr.IndexOf(']') + 1)];
                var logMessage = logEntryStr[(logEntryStr.IndexOf(']') + 1)..];

                yield return new LogEntry
                {
                    Date = parsedDate,
                    Level = level switch
                    {
                        "[Fatal]" => LogLevel.Fatal,
                        "[Error]" => LogLevel.Error,
                        "[Warning]" => LogLevel.Warning,
                        "[Information]" => LogLevel.Information,
                        _ => LogLevel.Information
                    },
                    Message = logMessage
                };
            }
        }
    }
}