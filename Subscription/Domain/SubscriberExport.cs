using CsvHelper.Configuration.Attributes;

namespace Subscription.Domain
{
    public class SubscriberExport
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string PostName { get; set; }
        public string Country { get; set; }

        public SubscriberExport(string lastName, string firstName, string address, string postCode, string postName, string country)
        {
            LastName = lastName;
            FirstName = firstName;
            Address = address;
            PostCode = postCode;
            PostName = postName;
            Country = country;
        }
    }
    public class SubscriberImport
    {
        [Index(0)]
        public long Id { get; set; }

        [Index(1)]
        public string LastName { get; set; }

        [Index(2)]
        public string FirstName { get; set; }

        [Index(3)]
        public string Address { get; set; }

        [Index(4)]
        public string PostCode { get; set; }

        [Index(5)]
        public string PostName { get; set; }

        [Index(6)]
        public string Country { get; set; }

        public SubscriberImport(
            long id, 
            string lastName, 
            string firstName, 
            string address, 
            string postCode, 
            string postName, 
            string country)
        {
            Id = id;
            LastName = lastName;
            FirstName = firstName;
            Address = address;
            PostCode = postCode;
            PostName = postName;
            Country = country;
        }
    }
}