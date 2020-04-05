using System;
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
        private readonly ExportDataSourceModel model;

        public ExportDataSourceDialog(YearAndMonth from, Func<ExportParams, Exceptional<Unit>> mergeAndExport)
        {
            InitializeComponent();
            this.mergeAndExport = mergeAndExport;
            model = new ExportDataSourceModel(from);
            DataContext = model;
        }

        private void BrowseMergeFileButton_OnClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                this.MergeFilePathTextBox.Text = openFileDialog.FileName;
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ExportButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var saveFileDialog = new SaveFileDialog
                {
                    FileName = "export.xlsx",
                    Filter = "Excel Worksheets|*.xlsx"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    var exportParams = new ExportParams(
                        new YearAndMonth(model.SelectedFromYear, model.SelectedFromMonth), 
                        model.CloneRowsForMultipleCopies, 
                        model.MergeFilePath,
                        saveFileDialog.FileName);
                    mergeAndExport(exportParams).Match(
                        Exception: (error) =>
                        {
                            MessageBox.Show(
                                this,
                                $"{Properties.Resources.ExportDataSourceError} ({error.Message})",
                                Properties.Resources.Error,
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                        },
                        Success: _ =>
                        {
                            MessageBox.Show(
                                this,
                                Properties.Resources.ExportDataSourceSuccess,
                                Properties.Resources.Information,
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                           Close();
                        });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
