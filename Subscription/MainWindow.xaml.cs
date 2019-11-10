using System.Windows;
using Subscription.Domain;
using Subscription.ViewModels;

namespace Subscription
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var model = new MainWindowModel(new Clock());
            DataContext = model;
        }
    }
}
