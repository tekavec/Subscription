using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Subscription.Domain;
using Subscription.Functional;

namespace Subscription.ViewModels
{
    public class MainWindowModel
    {
        public MainWindowModel()
        {
            Month.GetAllMonths().ForEach(a => Months.Add(a));
            Enumerable.Range(2019, DateTime.Now.Year - 2017).ForEach(a => Years.Add(a));
            var currentMonth = DateTime.Now.Month;
            SelectedMonth = new Month(currentMonth, DateTimeFormatInfo.CurrentInfo.GetMonthName(currentMonth));
            SelectedYear = DateTime.Now.Year;
        }

        public int SelectedYear { get; set; }

        public ObservableCollection<Month> Months { get; set; } = new ObservableCollection<Month>();

        public ObservableCollection<int> Years { get; set; } = new ObservableCollection<int>();

        public Month SelectedMonth { get; set; }

    }
}