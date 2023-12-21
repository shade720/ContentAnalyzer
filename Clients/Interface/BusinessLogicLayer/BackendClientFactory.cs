namespace ContentAnalyzer.Frontend.Desktop.BusinessLogicLayer;

public class BackendClientFactory
{
    public BackendClient? GetClient()
    {
        var configuration = ConfigurationManager.GetAppConfiguration();
        if (configuration is null || 
            string.IsNullOrEmpty(configuration.CurrentBackendHost) || 
            string.IsNullOrEmpty(configuration.BackendLogin) ||
            string.IsNullOrEmpty(configuration.BackendToken))
            return null;
        var backendClient = new BackendClient(configuration.CurrentBackendHost, configuration.BackendLogin, configuration.BackendToken);
        return backendClient;
    }
}