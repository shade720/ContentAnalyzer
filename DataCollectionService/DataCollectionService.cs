using Common;
using Common.EntityFramework;

namespace DataCollectionService;

public static class DataCollectionService
{
    private static readonly List<IDataCollector> DataCollectors = new();
    private static DatabaseClient _saveDatabase;

    public static List<CommentData> GetAllComments(int startIndex)
    {
        return _saveDatabase.GetRange<CommentData>(startIndex);
    }

    public static void RegisterSaveDatabase(DatabaseClient database)
    {
        _saveDatabase = database ?? throw new Exception("Save database cannot be null");
    }

    public static void AddDataCollector(Func<IDataCollector> dataCollectorConfiguration)
    {
        if (_saveDatabase is null) throw new NullReferenceException("Save database are not registered");
        DataCollectors.Add(dataCollectorConfiguration.Invoke());
        DataCollectors[^1].Subscribe(dataFrame => _saveDatabase.Add(dataFrame));
    }

    public static void Start()
    {
        if (_saveDatabase is null) throw new Exception("Save database are not registered");
        _saveDatabase.Connect();
        _saveDatabase.Clear();
        foreach (var dataCollector in DataCollectors)
        {
            dataCollector.StartCollecting();
        }
    }

    public static void Stop()
    {
        if (_saveDatabase is null) throw new Exception("Save database are not registered");
        foreach (var dataCollector in DataCollectors)
        {
            dataCollector.StopCollecting();
        }
        _saveDatabase.Disconnect();
    }
}