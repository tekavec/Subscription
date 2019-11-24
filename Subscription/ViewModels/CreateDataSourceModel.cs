using System.Collections.Generic;
using LaYumba.Functional;
using Subscription.Domain;

namespace Subscription.ViewModels
{
    public class CreateDataSourceModel
    {
        public CreateDataSourceModel(in YearAndMonth from)
        {
            SelectedFromYear = from.Year;
            SelectedFromMonth = from.Month;
            Month.GetAllMonths().ForEach(a => FromMonths.Add(a));
            FromYears.Add(from.Year);
            FromYears.Add(from.Year + 1);

            var toYearAndMonth = GetTargetPeriod(from);
            SelectedToYear = toYearAndMonth.Year;
            SelectedToMonth = toYearAndMonth.Month;
            Month.GetAllMonths().ForEach(a => ToMonths.Add(a));
            ToYears.Add(from.Year);
            ToYears.Add(from.Year + 1);

        }

        public IList<Month> FromMonths { get; set; } = new List<Month>();

        public Month SelectedFromMonth { get; set; }

        public IList<int> FromYears { get; set; } = new List<int>();

        public int SelectedFromYear { get; set; }

        public IList<Month> ToMonths { get; set; } = new List<Month>();

        public Month SelectedToMonth { get; set; }

        public IList<int> ToYears { get; set; } = new List<int>();

        public int SelectedToYear { get; set; }

        private static YearAndMonth GetTargetPeriod(YearAndMonth from)
        {
            var toMonth = from.Month.Number + 1;
            var toYear = from.Year;
            if (toMonth > 12)
            {
                toMonth = 1;
                toYear++;
            }

            return new YearAndMonth(toYear, new Month(toMonth));
        }
    }
}