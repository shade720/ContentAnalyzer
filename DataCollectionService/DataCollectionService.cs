using Interfaces;

namespace DataCollectionService;

public class DataCollectionService
{
    private readonly List<IDataCollector> _dataCollectors = new();

    public void AddDataCollector(Func<IDataCollector> dataCollectorConfiguration)
    {
        _dataCollectors.Add(dataCollectorConfiguration.Invoke());
    }

    public void Start()
    {
        foreach (var dataCollector in _dataCollectors)
        {
            dataCollector.StartCollecting();
        }
    }

    public void Stop()
    {
        foreach (var dataCollector in _dataCollectors)
        {
            dataCollector.StopCollecting();
        }
    }
}