namespace Interfaces;

public interface IPredictResult
{
    public IDataFrame DataFrame { get; init; }
    public string[] Predicts { get; init; }
}