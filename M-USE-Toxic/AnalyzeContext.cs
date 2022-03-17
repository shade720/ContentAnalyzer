using Interfaces;

namespace M_USE_Toxic;

internal class AnalyzeContextDictionary
{
    private readonly Dictionary<long,IDataFrame> _contextDataFrames = new();

    public void Add(IDataFrame dataFrame) => _contextDataFrames.Add(dataFrame.Id, dataFrame);

    public IDataFrame Get(long id) => _contextDataFrames[id];
}