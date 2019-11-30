using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Subscription.Domain;
using LaYumba.Functional;
using Subscription.Annotations;

namespace Subscription.ViewModels
{
    public class MainWindowModel : INotifyPropertyChanged
    {
        private ObservableCollection<Subscriber> subscribers;
        private const int AppStartYear = 2019;
        private const int ShowLastNumberOfYears = 2;
        public IList<Month> Months { get; set; } = new List<Month>();
        public Month SelectedMonth { get; set; }
        public IList<int> Years { get; set; } = new List<int>();
        public int SelectedYear { get; set; }

        public ObservableCollection<Subscriber> Subscribers
        {
            get => subscribers;
            set
            {
                subscribers = value;
                OnPropertyChanged(nameof(Subscribers));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        public MainWindowModel(IClock clock)
        {
            var now = clock.UtcNow;
            Month.GetAllMonths().ForEach(a => Months.Add(a));
            Enumerable.Range(2019, now.Year - (AppStartYear - ShowLastNumberOfYears)).ForEach(a => Years.Add(a));
            var currentMonth = now.Month;
            SelectedMonth = new Month(currentMonth);
            SelectedYear = now.Year;
            Subscribers = new ObservableCollection<Subscriber>();
        }

        public void OnMonthSelectionChanged()
        {
            var allSubscribers= SubscriberRepository.GetAll(GetSelectedYearAndMonth());
            Subscribers = new ObservableCollection<Subscriber>(allSubscribers);
        }

        private YearAndMonth GetSelectedYearAndMonth() => new YearAndMonth(SelectedYear, SelectedMonth);

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}