using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Subscription.Annotations;

namespace Subscription.Domain
{
    public class Subscriber : INotifyPropertyChanged
    {
        private string firstName;
        private string lastName;
        private string address;
        private string postCode;
        private string postName;
        private string country;
        private string subscriptionType;
        private string businessType;
        private int subscriptionCopies;
        private DateTime? subscriptionCancelledOn;
        private string cancellationReason;
        private bool isPaid;

        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string Address
        {
            get => address;
            set
            {
                address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        public string PostCode
        {
            get => postCode;
            set
            {
                postCode = value;
                OnPropertyChanged(nameof(PostCode));
            }
        }

        public string PostName
        {
            get => postName;
            set
            {
                postName = value;
                OnPropertyChanged(nameof(PostName));
            }
        }

        public string Country
        {
            get => country;
            set
            {
                country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        public string SubscriptionType
        {
            get => subscriptionType;
            set
            {
                subscriptionType = value;
                OnPropertyChanged(nameof(SubscriptionType));
            }
        }

        public string BusinessType
        {
            get => businessType;
            set
            {
                businessType = value;
                OnPropertyChanged(nameof(BusinessType));
            }
        }

        public int SubscriptionCopies
        {
            get => subscriptionCopies;
            set
            {
                subscriptionCopies = value;
                OnPropertyChanged(nameof(SubscriptionCopies));
            }
        }

        public DateTime? SubscriptionCancelledOn
        {
            get => subscriptionCancelledOn;
            set
            {
                subscriptionCancelledOn = value;
                OnPropertyChanged(nameof(SubscriptionCancelledOn));
            }
        }

        public string CancellationReason
        {
            get => cancellationReason;
            set
            {
                cancellationReason = value;
                OnPropertyChanged(nameof(CancellationReason));
            }
        }

        public bool IsPaid
        {
            get => isPaid;
            set
            {
                isPaid = value;
                OnPropertyChanged(nameof(IsPaid));
            }
        }

        public string ForFilterOnly =>
            $"{FirstName} {LastName} {Address} {PostCode} {PostName} {Country} {SubscriptionType} {BusinessType} {SubscriptionCancelledOn} {CancellationReason}".ToLower();

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void BeginEdit()
        {
        }

        public void CancelEdit()
        {
        }
    }
}