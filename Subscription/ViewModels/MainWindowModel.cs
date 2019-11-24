using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Subscription.Domain;
using LaYumba.Functional;

namespace Subscription.ViewModels
{
    public class MainWindowModel
    {
        private const int AppStartYear = 2019;
        private const int ShowLastNumberOfYears = 2;
        public IList<Month> Months { get; set; } = new List<Month>();
        public Month SelectedMonth { get; set; }
        public IList<int> Years { get; set; } = new List<int>();
        public int SelectedYear { get; set; }
        public IEnumerable<Subscriber> Subscribers { get; set; } 
        
        public MainWindowModel(IClock clock)
        {
            var now = clock.UtcNow;
            Month.GetAllMonths().ForEach(a => Months.Add(a));
            Enumerable.Range(2019, now.Year - (AppStartYear - ShowLastNumberOfYears)).ForEach(a => Years.Add(a));
            var currentMonth = now.Month;
            SelectedMonth = new Month(currentMonth);
            SelectedYear = now.Year;
            var subscribers = SubscriberRepository.GetAll(GetSelectedYearAndMonth());
            Subscribers = subscribers;
        }

        private YearAndMonth GetSelectedYearAndMonth()
        {
            return new YearAndMonth(SelectedYear, SelectedMonth);
        }
    }
}