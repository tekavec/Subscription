using System;

namespace Subscription.Domain
{
    public class Clock : IClock
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}