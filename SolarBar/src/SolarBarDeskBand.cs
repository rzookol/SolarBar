using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CSDeskBand;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SolarBar
{
    [ComVisible(true)]
    [Guid("AC63DE3B-1A75-4228-913A-4CFF0256F891")]
    [CSDeskBandRegistration(Name = "SolarBar", ShowDeskBand = true)]
    public class SolarBarDeskBand : CSDeskBand.CSDeskBandWin
    {
        private static Control control;
        private static ServiceProvider serviceProvider;
        public SolarBarDeskBand()
        {
           Options.MinHorizontalSize = new DeskBandSize(40,40);
           var serviceCollection = new ServiceCollection();
           var startup = new Startup();
           startup.ConfigureServices(serviceCollection);
           serviceProvider = serviceCollection.BuildServiceProvider();
           control = serviceProvider.GetRequiredService<Control>();
        }

        public IConfiguration Configuration { get; }

        protected override Control Control => control;
    }
}
