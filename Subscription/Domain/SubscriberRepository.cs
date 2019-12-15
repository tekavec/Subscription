using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ClosedXML.Excel;
using CsvHelper;
using LaYumba.Functional;
using static Subscription.Configuration.SettingManager;
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
            var file = GetFilePath(yearAndMonth);
            if (!File.Exists(file))
                return Enumerable.Empty<Subscriber>();

            using var reader = new StreamReader(file, Encoding.UTF8);
            using var csvReader = new CsvReader(reader, GetCsvConfiguration());
            return csvReader.GetRecords<Subscriber>().ToArray();
        }

        public static Exceptional<Unit> CopyDataSource(CopyFilesParams copyFilesParams)
        {
            try
            {
                var dataFolder = AppSettings.DataFolder;
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
            try
            {
                var internalSubscribersWithCopies = new List<SubscriberExport>();
                var externalSubscribers = Array.Empty<SubscriberImport>();
                if (!string.IsNullOrEmpty(exportParams.MergeFile))
                {
                    if (File.Exists(exportParams.MergeFile))
                    {
                        using (var reader = new StreamReader(exportParams.MergeFile, Encoding.GetEncoding(1250)))
                        {
                            reader.ReadLine();
                            using (var csvReader = new CsvReader(reader, GetImportCsvConfiguration()))
                            {
                                externalSubscribers = csvReader.GetRecords<SubscriberImport>().ToArray();
                            }

                        }
                    }

                }

                var internalSubscribers = GetAll(exportParams.FromYearAndMonth);

                if (exportParams.CloneRowsForMultipleCopies)
                {
                    foreach (var internalSubscriber in internalSubscribers.Where(a => a.IsPaid))
                    {
                        for (var i = 0; i < internalSubscriber.SubscriptionCopies; i++)
                        {
                            var subscriberExport = new SubscriberExport(
                                internalSubscriber.LastName,
                                internalSubscriber.FirstName,
                                internalSubscriber.Address,
                                internalSubscriber.PostCode,
                                internalSubscriber.PostName,
                                internalSubscriber.Country);
                            internalSubscribersWithCopies.Add(subscriberExport);
                        }
                    }
                }
                else
                {
                    internalSubscribersWithCopies = internalSubscribers
                        .Where(a => a.IsPaid)
                        .Select(a =>
                            new SubscriberExport(
                                a.LastName,
                                a.FirstName,
                                a.Address,
                                a.PostCode,
                                a.PostName,
                                a.Country))
                        .ToList();
                    
                }

                var subscribersToExportLength = internalSubscribersWithCopies.Count + externalSubscribers.Count();

                var subscribersToExport = new SubscriberExport[subscribersToExportLength];
                for (var i = 0; i < internalSubscribersWithCopies.Count; i++)
                {
                    subscribersToExport[i] = internalSubscribersWithCopies[i];
                }

                var externalCount = 0;
                externalSubscribers.ForEach(subscriber =>
                {
                    var subscriberExport = new SubscriberExport(
                        subscriber.LastName,
                        subscriber.FirstName,
                        subscriber.Address,
                        subscriber.PostCode,
                        subscriber.PostName,
                        subscriber.Country);
                    subscribersToExport[internalSubscribersWithCopies.Count + externalCount++] = subscriberExport;
                });

                var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Subscription");
                ws.Cell(1, 1).InsertTable(subscribersToExport
                    .OrderBy(a => a.Country)
                    .ThenBy(a => a.PostCode)
                    .ThenBy(a => a.LastName)
                    .AsEnumerable());
                wb.SaveAs(exportParams.ExportFile);
            }
            catch (Exception ex)
            {
                return ex;
            }

            return Unit();
        }

        public static Exceptional<Unit> Save(IEnumerable<Subscriber> subscribers, YearAndMonth yearAndMonth)
        {
            if (AppSettings.IsReadonly) return Unit();

            try
            {
                var file = GetFilePath(yearAndMonth);
                using var writer = new StreamWriter(file);
                using var csvWriter = new CsvWriter(writer, GetCsvConfiguration());
                csvWriter.WriteHeader<Subscriber>();
                csvWriter.NextRecord();
                csvWriter.WriteRecords(subscribers);
                csvWriter.Flush();
            }
            catch (Exception ex)
            {
                return ex;
            }

            return Unit();
        }

        private static CsvHelper.Configuration.Configuration GetCsvConfiguration() =>
            new CsvHelper.Configuration.Configuration
            {
                Delimiter = AppSettings.DataSourceDelimiter
            };

        private static CsvHelper.Configuration.Configuration GetImportCsvConfiguration() =>
            new CsvHelper.Configuration.Configuration
            {
                Delimiter = AppSettings.DataSourceDelimiter,
                HasHeaderRecord = false,
                IgnoreBlankLines = true,
                Encoding = Encoding.UTF8
            };

        private static string GetFilePath(YearAndMonth yearAndMonth)
        {
            var dataFolder = AppSettings.DataFolder;
            return Path.Combine(dataFolder,
                $"{SubscribersPrefix}_{yearAndMonth.Year}_{yearAndMonth.Month.Number:00}{SubscribersExtension}");
        }
    }
}