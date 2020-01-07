using System.ComponentModel;
using System.Text;
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
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            if (UserRepository.IsUserRegistered())
            {
                var login = new LoginDialog();
                var loginResult = login.ShowDialog();
                if (loginResult == null || loginResult == false)
                {
                    Application.Current.Shutdown();
                }
            }
            else
            {
                var registration = new RegistrationDialog();
                var registrationResult = registration.ShowDialog();
                if (registrationResult == null || registrationResult == false)
                {
                    Application.Current.Shutdown();
                }
            }

            model = new MainWindowModel(new Clock());
            DataContext = model;
        }

        private void Term_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            model.SavePreviousSelected();
            model.OnMonthSelectionChanged();
            if (model.Subscribers.Count == 0)
            {
                if (MessageBox.Show(
                        Properties.Resources.NoSubscribersForCurrentTerm,
                        Properties.Resources.Question,
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    OpenCreateNewDataSource();
                }
            }
        }

        private void CreateNewDataSource_OnClick(object sender, RoutedEventArgs e)
        {
            Save();
            OpenCreateNewDataSource();
        }

        private void OpenCreateNewDataSource()
        {
            var createDataSource =
                new CreateDataSourceDialog(GetCopyFromToParams(), SubscriberRepository.CopyDataSource);
            createDataSource.ShowDialog();
        }

        private void ExportDataSource_OnClick(object sender, RoutedEventArgs e)
        {
            Save();
            var exportDataSourceDialog = 
                new ExportDataSourceDialog(GetCopyFromToParams(), SubscriberRepository.ExportDataSource);
            exportDataSourceDialog.ShowDialog();
        }

        private void SaveDataSource_OnClick(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void Save()
        {
            DataGridCellInfo lastCell = new DataGridCellInfo(SubscribersDataGrid.SelectedItem, this.CancellationReasonColumn);
            DataGridCellInfo firstCell = new DataGridCellInfo(SubscribersDataGrid.SelectedItem, this.FirstNameColumn);
            this.SubscribersDataGrid.CurrentCell = lastCell;
            this.SubscribersDataGrid.CurrentCell = firstCell;
            SubscribersDataGrid.ScrollIntoView(SubscribersDataGrid.SelectedItem, this.FirstNameColumn);
            SubscribersDataGrid.UpdateLayout();
            model.SaveDataSource();
        }

        private YearAndMonth GetCopyFromToParams() => 
            new YearAndMonth(model.SelectedYear, model.SelectedMonth);

        private void SubscribersDataGrid_OnAddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            model.MakeDirty();
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(
                    Properties.Resources.DeleteConfirmation,
                    Properties.Resources.Question,
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (SubscribersDataGrid.SelectedItem is Subscriber subscriber)
                {
                    model.Subscribers.Remove(subscriber);
                    model.MakeDirty();
                }

                SubscribersDataGrid.Items.Refresh();
            }
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            Save();
        }

        private void SubscribersDataGrid_OnPreparingCellForEdit(object sender, DataGridPreparingCellForEditEventArgs e)
        {
            model.MakeDirty();
        }

        private void ClearFilterButton_OnClick(object sender, RoutedEventArgs e)
        {
            model.FilterString = string.Empty;
        }

        private void NewSubscriber_OnClick(object sender, RoutedEventArgs e)
        {
            SubscribersDataGrid.SelectedIndex = SubscribersDataGrid.Items.Count-1;
            SubscribersDataGrid.ScrollIntoView(SubscribersDataGrid.Items[^1]);
            SubscribersDataGrid.ScrollIntoView(SubscribersDataGrid.Items[^1], this.FirstNameColumn);
            SubscribersDataGrid.UpdateLayout();
            var firstCell = new DataGridCellInfo(SubscribersDataGrid.SelectedItem, this.FirstNameColumn);
            SubscribersDataGrid.CurrentCell = firstCell;
            SubscribersDataGrid.BeginEdit();
        }

        private void SubscribersDataGrid_OnLoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void CountryComboBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            model.RefreshCountries();
        }

        private void SubscriptionTypeComboBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            model.RefreshSubscriptionTypes();
        }

        private void BusinessTypeComboBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            model.RefreshBusinessTypes();
        }
    }
}
