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
        public MainWindowModel(IClock clock)
        {
            var now = clock.UtcNow;
            Month.GetAllMonths().ForEach(a => Months.Add(a));
            Enumerable.Range(2019, now.Year - (AppStartYear - ShowLastNumberOfYears)).ForEach(a => Years.Add(a));
            var currentMonth = now.Month;
            SelectedMonth = new Month(currentMonth);
            SelectedYear = now.Year;
            previousSelectedMonth = SelectedMonth;
            previousSelectedYear = SelectedYear;
            Subscribers = new ObservableCollection<Subscriber>();
        }

        private ObservableCollection<Subscriber> subscribers;

        private const int AppStartYear = 2019;

        private const int ShowLastNumberOfYears = 2;

        private Month previousSelectedMonth;

        private int previousSelectedYear;

        private bool isDirty = false;

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

        public void OnMonthSelectionChanged()
        {
            var allSubscribers= SubscriberRepository.GetAll(GetSelectedYearAndMonth());
            allSubscribers.ForEach(a => a.PropertyChanged += Subscriber_PropertyChanged);
            Subscribers = new ObservableCollection<Subscriber>(allSubscribers);
            isDirty = false;
        }

        private void Subscriber_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            isDirty = true;
        }

        private YearAndMonth GetSelectedYearAndMonth() => new YearAndMonth(SelectedYear, SelectedMonth);

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SaveDataSource()
        {
            if (!isDirty) return;
            SubscriberRepository.Save(Subscribers.AsEnumerable(), GetSelectedYearAndMonth());
            isDirty = false;
        }

        public void SavePreviousSelected()
        {
            if (isDirty)
            {
                SubscriberRepository.Save(
                    Subscribers.AsEnumerable(),
                    new YearAndMonth(previousSelectedYear, previousSelectedMonth));
            }
            
            previousSelectedMonth = SelectedMonth;
            previousSelectedYear = SelectedYear;

            isDirty = false;
        }

        public void MakeDirty()
        {
            isDirty = true;
        }
    }
}