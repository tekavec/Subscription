namespace Subscription.Domain
{
    public class SubscriberExport
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string PostName { get; set; }
        public string Country { get; set; }

        public SubscriberExport(string firstName, string lastName, string address, string postCode, string postName, string country)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            PostCode = postCode;
            PostName = postName;
            Country = country;
        }
    }
}