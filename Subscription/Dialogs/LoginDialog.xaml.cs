using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Subscription.Domain;
using LaYumba.Functional;
using Unit = System.ValueTuple;

namespace Subscription.Dialogs
{
    public partial class LoginDialog : Window
    {

        public LoginDialog()
        {
            InitializeComponent();
        }

        private void PinPasswordBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                UserRepository
                    .ValidateLogin(this.PinPasswordBox.Password)
                    .Match(
                        Invalid: InvalidLogin,
                        Valid: LoginOk);
            }
        }

        private void LoginOk(ValueTuple obj)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void InvalidLogin(IEnumerable<Error> errors)
        {
            var errorMessages = new StringBuilder(Properties.Resources.LoginFailed);
            errors.ForEach(error => errorMessages.AppendLine(error.Message));
            MessageBox.Show(
                this,
                errorMessages.ToString(),
                Properties.Resources.Error,
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}
