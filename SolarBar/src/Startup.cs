using LiteDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SolarBar.Config;
using SolarBar.HttpClient;
using SolarBar.Model;
using SolarBar.Services;
using SolarBar.UI;
using System;
using System.IO;
using System.Windows.Forms;

public class Startup
{
    public IConfiguration Configuration { get; }
    public void ConfigureServices(IServiceCollection services)
    {
        var configuration = LoadConfiguration();
        services.AddSingleton(configuration);

        var section = configuration.GetSection(nameof(BarConfig));
        var serviceConfig = new BarConfig();

        serviceConfig.ApiKey = section["ApiKey"];
        serviceConfig.SiteId = section["SiteId"];
        serviceConfig.Host   = section["Host"];
        serviceConfig.ChartStartHour = int.Parse(section["ChartStartHour"]);
        serviceConfig.ChartEndHour = int.Parse(section["ChartEndHour"]);

        serviceConfig.ConfigPath = Path.Combine(GetConfigPath(), "configuration.json");

        services.AddSingleton(serviceConfig);

        string dbPath = Path.Combine(GetConfigPath(), "solarbar.litedb");
        string connectionString = $"Data Source={dbPath};";


        using (var db = new LiteDatabase(dbPath))
        {
            // Get a collection (or create, if doesn't exist)
            var col = db.GetCollection<Energy>("energy");
        }

        services.AddOptions();
        services.AddHttpClient("SolarEdgeHttpClient");
        services.AddTransient<ISolarEdgeHttpClient, SolarEdgeHttpClient>();
        services.AddTransient<IProxyService, ProxyService>();
        services.AddSingleton<Control, MainView>();
    }

    private static string GetConfigPath()
    {
       return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SolarBar");
    }

    private static IConfiguration LoadConfiguration()
    {
        string appDataFolder = GetConfigPath();
        Directory.CreateDirectory(appDataFolder);

        var builder = new ConfigurationBuilder()
          .SetBasePath(appDataFolder)
          .AddJsonFile("configuration.json", optional: true, reloadOnChange: true);

        return builder.Build();
    }
}