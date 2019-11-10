using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Subscription.Domain;
using Subscription.Functional;

namespace Subscription.ViewModels
{
    public class MainWindowModel
    {
        private const int AppStartYear = 2019;
        private const int ShowLastNumberOfYears = 2;
        public MainWindowModel(IClock clock)
        {
            var now = clock.UtcNow;
            Month.GetAllMonths().ForEach(a => Months.Add(a));
            Enumerable.Range(2019, now.Year - (AppStartYear - ShowLastNumberOfYears)).ForEach(a => Years.Add(a));
            var currentMonth = now.Month;
            SelectedMonth = new Month(currentMonth, DateTimeFormatInfo.CurrentInfo.GetMonthName(currentMonth));
            SelectedYear = now.Year;
        }

        public int SelectedYear { get; set; }

        public ObservableCollection<Month> Months { get; set; } = new ObservableCollection<Month>();

        public ObservableCollection<int> Years { get; set; } = new ObservableCollection<int>();

        public Month SelectedMonth { get; set; }

    }
}