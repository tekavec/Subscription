using System.Collections.Generic;
using LaYumba.Functional;
using Subscription.Domain;

namespace Subscription.ViewModels
{
    public class ExportDataSourceModel
    {
        public ExportDataSourceModel(in YearAndMonth from)
        {
            SelectedFromYear = from.Year;
            SelectedFromMonth = from.Month;
            Month.GetAllMonths().ForEach(a => FromMonths.Add(a));
            FromYears.Add(from.Year);
            FromYears.Add(from.Year + 1);
            CloneRowsForMultipleCopies = true;
            MergeFilePath = string.Empty;
        }

        public IList<Month> FromMonths { get; set; } = new List<Month>();
        public Month SelectedFromMonth { get; set; }
        public IList<int> FromYears { get; set; } = new List<int>();
        public int SelectedFromYear { get; set; }
        public bool CloneRowsForMultipleCopies { get; set; }
        public string MergeFilePath { get; set; }
    }
}