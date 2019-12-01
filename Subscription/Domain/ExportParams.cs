namespace Subscription.Domain
{
    public class ExportParams
    {
        public YearAndMonth FromYearAndMonth { get; }
        public bool CloneRowsForMultipleCopies { get; }
        public string MergeFile { get; }

        public ExportParams(
            YearAndMonth fromYearAndMonth, 
            bool cloneRowsForMultipleCopies, 
            string mergeFile = "")
        {
            FromYearAndMonth = fromYearAndMonth;
            CloneRowsForMultipleCopies = cloneRowsForMultipleCopies;
            MergeFile = mergeFile;
        }
    }
}