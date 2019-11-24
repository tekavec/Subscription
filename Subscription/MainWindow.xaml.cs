using System.Windows;
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

        private void CreateNewDataSource_OnClick(object sender, RoutedEventArgs e)
        {
            var createDataSource = new CreateDataSourceDialog(GetCopyFromToParams(), SubscriberRepository.CopyDataSource);
            createDataSource.ShowDialog();
        }

        private YearAndMonth GetCopyFromToParams() => 
            new YearAndMonth(
                model.Years[this.YearsComboBox.SelectedIndex], 
                model.Months[this.MonthsListBox.SelectedIndex]);
    }
}
