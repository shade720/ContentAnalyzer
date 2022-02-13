namespace Interfaces;

public interface IDataAnalyzer
{
    public void Initialize();

    public void Analyze(IDataFrame text);

    public void Dispose();
}