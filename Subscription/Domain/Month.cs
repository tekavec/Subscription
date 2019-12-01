using System;
using System.Collections.Generic;
using System.Globalization;

namespace Subscription.Domain
{
    public struct Month : IEquatable<Month>
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

        public bool Equals(Month other) => 
            Number == other.Number && Name == other.Name;

        public override bool Equals(object obj) => 
            obj is Month other && Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                return (Number * 397) ^ (Name != null ? Name.GetHashCode() : 0);
            }
        }
    }
}