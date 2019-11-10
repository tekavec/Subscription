using System.Collections.Generic;
using System.Globalization;

namespace Subscription.Domain
{
    public struct Month
    {
        public Month(int number, string name)
        {
            Number = number;
            Name = name;
        }

        public int Number { get; }
        public string Name { get; }
        public override string ToString() => Name;

        public static IEnumerable<Month> GetAllMonths()
        {
            for (var i = 1; i <= 12; i++)
            {
                yield return new Month (i, DateTimeFormatInfo.CurrentInfo.GetMonthName(i));
            }
        }
    }
}