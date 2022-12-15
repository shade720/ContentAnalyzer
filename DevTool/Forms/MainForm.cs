using DevTool.Models;

namespace DevTool.Forms
{
    public partial class MainForm : Form
    {
        private readonly ServiceManager _serviceManager;
        public MainForm(ServiceManager serviceManager)
        {
            InitializeComponent();
            _serviceManager = serviceManager;
        }

        #region ButtonHandlers

        private void StopCollectionService_Click(object sender, EventArgs e)
        {
            _serviceManager.StopDataCollectionService();
        }

        private void StopAnalysisService_Click(object sender, EventArgs e)
        {
            _serviceManager.StopDataAnalysisService();
        }

        private void StartCollectionService_Click(object sender, EventArgs e)
        {
            _serviceManager.StartDataCollectionService();
        }

        private void StartAnalysisService_Click(object sender, EventArgs e)
        {
            _serviceManager.StartDataAnalysisService();
        }

        private void ClearCommentsDatabase_Click(object sender, EventArgs e)
        {

        }

        private void ClearEvaluatedDatabase_Click(object sender, EventArgs e)
        {

        }

        private void StartAll_Click(object sender, EventArgs e)
        {
            _serviceManager.StartDataAnalysisService();
            _serviceManager.StartDataCollectionService();
        }

        private void StopAll_Click(object sender, EventArgs e)
        {
            _serviceManager.StopDataCollectionService();
            _serviceManager.StopDataAnalysisService();
        }

        private void ViewCollectionServiceLogs_Click(object sender, EventArgs e)
        {
            
        }

        private void ViewAnalysisServiceLogs_Click(object sender, EventArgs e)
        {

        }

        private void ApplyNewCollectionServiceEndpoint_Click(object sender, EventArgs e)
        {
            _serviceManager.SetCollectionServiceHost(CollectionServiceEndpoint.Text);
        }

        private void ApplyAnalysisServiceEndpoint_Click(object sender, EventArgs e)
        {
            _serviceManager.SetCollectionServiceHost(AnalysisServiceEndpoint.Text);
        }

        private void RefreshCollectionServiceInfo_Click(object sender, EventArgs e)
        {
            var info = _serviceManager.PollCollectionService();

        }

        private void RefreshAnalysisServiceInfo_Click(object sender, EventArgs e)
        {
            var info = _serviceManager.PollAnalysisService();
        }

        #endregion
    }
}