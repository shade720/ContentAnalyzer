using Interfaces;

namespace M_USE_Toxic;

internal class AnalyzeContextStack
{
    private readonly Stack<IDataFrame> _contextDataFrames = new();

    public void Push(IDataFrame dataFrame) => _contextDataFrames.Push(dataFrame);

    public IDataFrame Pop() => _contextDataFrames.Pop();
}