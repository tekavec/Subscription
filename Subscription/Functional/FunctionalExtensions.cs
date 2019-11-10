using System;
using System.Collections;
using System.Collections.Generic;

namespace Subscription.Functional
{
    public static class FunctionalExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> self, Action<T> action)
        {
            foreach (var item in self)
            {
                action(item);
            }
        }
    }
}