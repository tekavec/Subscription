namespace Subscription.Configuration
{
    public class AppSetting
    {
        public bool IsReadonly { get; set; }
        public string CultureInfo { get; set; }
        public int SelectedYear { get; set; }
        public int SelectedMonth { get; set; }
        public string DataFolder { get; set; }
        public string DataSourceDelimiter { get; set; }
    }
}