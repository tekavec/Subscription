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

        private void MonthsListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            model.SavePreviousSelected();
            model.OnMonthSelectionChanged();
        }

        private void CreateNewDataSource_OnClick(object sender, RoutedEventArgs e)
        {
            Save();
            var createDataSource = new CreateDataSourceDialog(GetCopyFromToParams(), SubscriberRepository.CopyDataSource);
            createDataSource.ShowDialog();
        }

        private void ExportDataSource_OnClick(object sender, RoutedEventArgs e)
        {
            Save();
            var exportDataSourceDialog = new ExportDataSourceDialog(GetCopyFromToParams(), SubscriberRepository.ExportDataSource);
            exportDataSourceDialog.ShowDialog();
        }

        private void SaveDataSource_OnClick(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void Save()
        {
            model.SaveDataSource();
        }

        private YearAndMonth GetCopyFromToParams() => 
            new YearAndMonth(
                model.Years[this.YearsComboBox.SelectedIndex], 
                model.Months[this.MonthsListBox.SelectedIndex]);

        private void SubscribersDataGrid_OnAddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            model.MakeDirty();
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(
                    "Do you want to delete this row?",
                    "Question",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                var subscriber = this.SubscribersDataGrid.SelectedItem;
                if (subscriber != null)
                {
                    model.Subscribers.Remove((Subscriber) subscriber);
                    model.MakeDirty();
                }
            }
        }
    }
}
