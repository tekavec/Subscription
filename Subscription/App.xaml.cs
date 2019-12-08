using System.Configuration;
using System.Windows;

namespace Subscription
{
    public partial class App : Application
    {
        public App()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = 
                new System.Globalization.CultureInfo(ConfigurationManager.AppSettings["CultureInfo"]);
        }
    }
}
