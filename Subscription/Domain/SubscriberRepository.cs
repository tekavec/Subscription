using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using LaYumba.Functional;
using Subscription.Dialogs;
using static LaYumba.Functional.F;
using Unit = System.ValueTuple;

namespace Subscription.Domain
{
    public class SubscriberRepository
    {
        private const string SubscribersPrefix = "subscribers";
        private const string SubscribersExtension = ".csv";
        
        public static IEnumerable<Subscriber> GetAll(YearAndMonth yearAndMonth)
        {
            var dataFolder = ConfigurationManager.AppSettings["DataFolder"];
            var file = Path.Combine(dataFolder, $"{SubscribersPrefix}_{yearAndMonth.Year}_{yearAndMonth.Month.Number:00}{SubscribersExtension}");
            if (!File.Exists(file))
                return Enumerable.Empty<Subscriber>();

            using var reader = new StreamReader(file, Encoding.UTF8);
            var csvReader = new CsvReader(reader);
            var records = csvReader.GetRecords<Subscriber>();
            return records.ToArray();
        }

        public static Exceptional<Unit> CopyDataSource(CopyFilesParams copyFilesParams)
        {
            try
            {
                var dataFolder = ConfigurationManager.AppSettings["DataFolder"];
                var sourceFile = Path.Combine(dataFolder, $"{SubscribersPrefix}_{copyFilesParams.From.Year}_{copyFilesParams.From.Month.Number:00}{SubscribersExtension}");
                if (!File.Exists(sourceFile))
                    return new FileNotFoundException("Source file not found.", sourceFile);
                
                var destinationFile = Path.Combine(dataFolder, $"{SubscribersPrefix}_{copyFilesParams.To.Year}_{copyFilesParams.To.Month.Number:00}{SubscribersExtension}");
                if (File.Exists(destinationFile))
                    return new ArgumentException("Target file already exists.", destinationFile);

                File.Copy(sourceFile, destinationFile);
            }
            catch (Exception ex)
            {
                return ex;
            }

            return Unit();
        }

        public static Exceptional<Unit> ExportDataSource(ExportParams exportParams)
        {
            return Unit();
        }
    }
}