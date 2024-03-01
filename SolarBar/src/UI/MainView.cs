using System;
using System.Windows.Forms;
using SolarBar.Config;
using SolarBar.Services;

namespace SolarBar.UI
{
    public partial class MainView : UserControl
    {
        IProxyService _proxyService;
        BarConfig _config;

        public MainView(IProxyService proxyService, BarConfig config)
        {
            _proxyService = proxyService;
            _config = config;

            InitializeComponent();

            Load += MainView_Load;
        }


        private void MainView_Load(object sender, EventArgs e)
        {
            ucSolarBar1.Run(_proxyService, _config);
        }

        private void ucSolarBar1_Load(object sender, EventArgs e)
        {

        }
    }
}
