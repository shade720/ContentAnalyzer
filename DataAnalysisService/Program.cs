namespace DataAnalysisService;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

public static class MainClass
{
    public static void Main()
    {
        var engine = Python.CreateEngine();
        engine.Execute("print 'hello, world'");
    }
}