using System;

namespace Subscription.Domain
{
    public interface IClock
    {
        DateTime UtcNow { get;  }
    }
}