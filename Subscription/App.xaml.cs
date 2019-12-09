using System.IO;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Subscription.Configuration;

namespace Subscription
{
    public partial class App : Application
    {
        public App()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();

            var appSettings = new AppSetting();
            configuration.GetSection("AppSettings").Bind(appSettings);
            SettingManager.AppSettings = appSettings;

            System.Threading.Thread.CurrentThread.CurrentUICulture = 
                new System.Globalization.CultureInfo(appSettings.CultureInfo);
        }
    }
}
