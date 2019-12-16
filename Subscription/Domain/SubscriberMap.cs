using CsvHelper.Configuration;

namespace Subscription.Domain
{
    public sealed class SubscriberMap : ClassMap<Subscriber>
    {
        public SubscriberMap()
        {
            AutoMap();
            Map(m => m.ForFilterOnly).Ignore();
        }
    }
}