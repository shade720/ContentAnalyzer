using Interfaces;

namespace DataCollectionService;

public static class DataCollectionService
{
    private static readonly List<IDataCollector> DataCollectors = new();
    private static IDatabaseClient? _saveDatabase;

    public static void RegisterSaveDatabase(IDatabaseClient? database)
    {
        _saveDatabase = database;
    }

    public static void AddDataCollector(Func<IDataCollector> dataCollectorConfiguration)
    {
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
        _saveDatabase.Disconnect();
        foreach (var dataCollector in DataCollectors)
        {
            dataCollector.StopCollecting();
        }
    }
}