using System.Globalization;
using CsvHelper.Configuration;

namespace Subscription.Domain
{
    public sealed class SubscriberMap : ClassMap<Subscriber>
    {
        public SubscriberMap()
        {
            AutoMap(CultureInfo.CurrentCulture);
            Map(m => m.ForFilterOnly).Ignore();
        }
    }
}