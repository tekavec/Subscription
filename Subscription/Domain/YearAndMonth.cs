namespace Subscription.Domain
{
    public class YearAndMonth
    {
        public int Year { get; }
        public Month Month { get; }

        public YearAndMonth(int year, Month month)
        {
            Month = month;
            Year = year;
        }
    }
}