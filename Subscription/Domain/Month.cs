using System.Collections.Generic;
using System.Globalization;

namespace Subscription.Domain
{
    public struct Month
    {
        public Month(int number)
        {
            Number = number;
            Name = DateTimeFormatInfo.CurrentInfo.GetMonthName(number);
        }

        public int Number { get; }
        public string Name { get; }
        public override string ToString() => Name;

        public static IEnumerable<Month> GetAllMonths()
        {
            for (var i = 1; i <= 12; i++)
            {
                yield return new Month (i);
            }
        }
    }
}