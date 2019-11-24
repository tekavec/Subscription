namespace Subscription.Domain
{
    public class CopyFilesParams
    {
        public YearAndMonth From { get; }
        public YearAndMonth To { get; }

        public CopyFilesParams(YearAndMonth fromParams, YearAndMonth toParams)
        {
            From = fromParams;
            To = toParams;
        }
    }
}