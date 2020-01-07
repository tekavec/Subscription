using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Data;
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

        public bool Filter(object obj)
        {
            if (obj is Subscriber data)
            {
                if (!string.IsNullOrEmpty(filterString))
                {
                    return data.ForFilterOnly.Contains(filterString.ToLower());
                }
                return true;
            }
            return false;
        }

        public string FilterString
        {
            get => filterString;
            set
            {
                filterString = value;
                OnPropertyChanged(nameof(FilterString));
                FilterCollection();
            }
        }

        private void FilterCollection()
        {
            subscribersView?.Refresh();
        }

        private ObservableCollection<Subscriber> subscribers;

        private ICollectionView subscribersView;

        private string filterString;

        private const int AppStartYear = 2019;

        private const int ShowLastNumberOfYears = 2;

        private Month previousSelectedMonth;

        private int previousSelectedYear;

        private bool isDirty = false;

        public IList<Month> Months { get; set; } = new List<Month>();

        public Month SelectedMonth { get; set; }

        public IList<int> Years { get; set; } = new List<int>();

        public int SelectedYear { get; set; }

        public string AppName => $"{Properties.Resources.AppName} (v{Version})";
        
        public string Version => GetType().Assembly.GetName().Version.ToString();

        public ObservableCollection<Subscriber> Subscribers
        {
            get => subscribers;
            set
            {
                subscribers = value;
                OnPropertyChanged(nameof(Subscribers));
            }
        }

        public IList<string> Countries { get; private set; }
        public IList<string> SubscriptionTypes { get; private set; }
        public IList<string> BusinessTypes { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnMonthSelectionChanged()
        {
            var allSubscribers= SubscriberRepository.GetAll(GetSelectedYearAndMonth());
            allSubscribers.ForEach(a => a.PropertyChanged += Subscriber_PropertyChanged);
            Subscribers = new ObservableCollection<Subscriber>(allSubscribers);
            if (Subscribers.Count > 0)
            {
                subscribersView = CollectionViewSource.GetDefaultView(Subscribers);
                subscribersView.Filter = Filter;
                RefreshLookupValues();
            }

            isDirty = false;
        }

        private void RefreshLookupValues()
        {
            RefreshCountries();
            RefreshSubscriptionTypes();
            RefreshBusinessTypes();
        }

        internal void RefreshCountries()
        {
            Countries = Subscribers.Select(a => a.Country).Distinct().OrderBy(a => a).ToList();
            OnPropertyChanged(nameof(Countries));
        }

        internal void RefreshSubscriptionTypes()
        {
            SubscriptionTypes = Subscribers.Select(a => a.SubscriptionType).Distinct().OrderBy(a => a).ToList();
            OnPropertyChanged(nameof(SubscriptionTypes));
        }

        internal void RefreshBusinessTypes()
        {
            BusinessTypes = Subscribers.Select(a => a.BusinessType).Distinct().OrderBy(a => a).ToList();
            OnPropertyChanged(nameof(BusinessTypes));
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