using System.Windows;
using System.Windows.Controls;
using Subscription.Dialogs;
using Subscription.Domain;
using Subscription.ViewModels;

namespace Subscription
{
    public partial class MainWindow : Window
    {
        private readonly MainWindowModel model;
        public MainWindow()
        {
            InitializeComponent();
            model = new MainWindowModel(new Clock());
            DataContext = model;
        }

        private void CreateNewDataSourceButton_OnClick(object sender, RoutedEventArgs e)
        {
            var createDataSource = new CreateDataSourceDialog(GetCopyFromToParams(), SubscriberRepository.CopyDataSource);
            createDataSource.ShowDialog();
        }

        private YearAndMonth GetCopyFromToParams() => 
            new YearAndMonth(
                model.Years[this.YearsComboBox.SelectedIndex], 
                model.Months[this.MonthsListBox.SelectedIndex]);

        private void ExportDataSourceButton_OnClick(object sender, RoutedEventArgs e)
        {
            var exportDataSourceDialog = new ExportDataSourceDialog(GetCopyFromToParams(), SubscriberRepository.ExportDataSource);
            exportDataSourceDialog.ShowDialog();
        }

        private void MonthsListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            model.OnMonthSelectionChanged();
        }

        private void SaveDataSourceButton_OnClick(object sender, RoutedEventArgs e)
        {
            model.SaveDataSource();
        }
    }
}
