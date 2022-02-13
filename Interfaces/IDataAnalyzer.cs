namespace Interfaces;

public interface IDataAnalyzer
{
    public void Initialize();

    public void Analyze(string text);

    public void Dispose();
}