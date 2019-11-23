using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using LaYumba.Functional;

namespace Subscription.Domain
{
    public class SubscriberRepository
    {
        private const string SubscribersFileName = "subscribers.csv";
        public static IEnumerable<Subscriber> GetAll()
        {
            var dataFolder = ConfigurationManager.AppSettings["DataFolder"];
            var file = Path.Combine(dataFolder, SubscribersFileName);
            if (!File.Exists(file))
                return Enumerable.Empty<Subscriber>();

            using var reader = new StreamReader(file, Encoding.UTF8);
            var csvReader = new CsvReader(reader);
            var records = csvReader.GetRecords<Subscriber>();
            return records.ToArray();
        }
    }
}