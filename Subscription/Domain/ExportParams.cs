namespace Subscription.Domain
{
    public class ExportParams
    {
        public YearAndMonth FromYearAndMonth { get; }
        public bool CloneRowsForMultipleCopies { get; }
        public string MergeFile { get; }
        public string ExportFile { get; }

        public ExportParams(
            YearAndMonth fromYearAndMonth, 
            bool cloneRowsForMultipleCopies, 
            string mergeFile = "",
            string exportFile = "")
        {
            FromYearAndMonth = fromYearAndMonth;
            CloneRowsForMultipleCopies = true;
            MergeFile = mergeFile;
            ExportFile = exportFile;
        }
    }
}