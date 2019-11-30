using System;
using System.IO;
using System.Windows;
using Subscription.Domain;
using Subscription.ViewModels;
using LaYumba.Functional;
using Microsoft.Win32;
using Unit = System.ValueTuple;

namespace Subscription.Dialogs
{
    public partial class ExportDataSourceDialog : Window
    {
        private readonly Func<ExportParams, Exceptional<Unit>> mergeAndExport;
        private readonly CreateDataSourceModel model;

        public ExportDataSourceDialog(YearAndMonth from, Func<ExportParams, Exceptional<Unit>> mergeAndExport)
        {
            this.mergeAndExport = mergeAndExport;
            InitializeComponent();
            model = new CreateDataSourceModel(from);
            DataContext = model;
        }

        private void BrowseMergeFileButton_OnClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                MergeFilePathTextBox.Text = openFileDialog.FileName;
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ExportButton_OnClick(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                FileName = "sasaasas.xlsx",
                Filter = "Excel Worksheets|*.xlsx"
            };
            if (saveFileDialog.ShowDialog() == true)
            {

            }
        }
    }

    public class ExportParams
    {
        public YearAndMonth FromYearAndMonth { get; }
        public bool CloneRowsForMultipleCopies { get; }
        public string MergeFile { get; }

        public ExportParams(
            YearAndMonth fromYearAndMonth, 
            bool isMergeRequired, 
            bool cloneRowsForMultipleCopies, 
            string mergeFile = "")
        {
            FromYearAndMonth = fromYearAndMonth;
            CloneRowsForMultipleCopies = cloneRowsForMultipleCopies;
            MergeFile = mergeFile;
        }
    }
}
