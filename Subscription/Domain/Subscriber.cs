namespace Subscription.Domain
{
    public class Subscriber
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string PostName { get; set; }
        public string Country { get; set; }
        public string SubscriptionType { get; set; }
        public string BusinessType { get; set; }
        public int SubscriptionCopies { get; set; }
        public string SubscriptionCancelledOn { get; set; }
        public string CancellationReason { get; set; }
        public bool IsPaid { get; set; }
    }
}