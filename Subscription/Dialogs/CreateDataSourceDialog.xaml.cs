using System;
using System.Windows;
using Subscription.Domain;
using Subscription.ViewModels;
using LaYumba.Functional;
using Unit = System.ValueTuple;

namespace Subscription.Dialogs
{
    public partial class CreateDataSourceDialog : Window
    {
        private readonly Func<CopyFilesParams, Exceptional<Unit>> copyFiles;
        private readonly CreateDataSourceModel model;

        public CreateDataSourceDialog(YearAndMonth from, Func<CopyFilesParams, Exceptional<Unit>> copyFiles)
        {
            this.copyFiles = copyFiles;
            InitializeComponent();
            model = new CreateDataSourceModel(from);
            DataContext = model;
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CreateNewMonthlySubscriptionButton_OnClick(object sender, RoutedEventArgs e)
        {
            copyFiles(GetCopyFilesParams())
                .Match(
                    ex => MessageBox.Show(
                        this, 
                        ex.Message, 
                        Properties.Resources.Error, 
                        MessageBoxButton.OK, 
                        MessageBoxImage.Error),
                    _ =>
                    {
                        MessageBox.Show(
                            this,
                            Properties.Resources.NewDataSourceCreateSuccess,
                            Properties.Resources.Information,
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                        this.Close();
                    });
        }

        private CopyFilesParams GetCopyFilesParams() =>
            new CopyFilesParams(GetFromParams(), GetToParams());

        private YearAndMonth GetFromParams() =>
            new YearAndMonth(
                model.SelectedFromYear,
                model.SelectedFromMonth);

        private YearAndMonth GetToParams() =>
            new YearAndMonth(
                model.SelectedToYear,
                model.SelectedToMonth);
    }
}
