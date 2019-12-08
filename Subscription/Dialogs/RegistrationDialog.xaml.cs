using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Subscription.Domain;
using LaYumba.Functional;
using Unit = System.ValueTuple;

namespace Subscription.Dialogs
{
    public partial class RegistrationDialog : Window
    {

        public RegistrationDialog()
        {
            InitializeComponent();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            var registrationModel = new RegistrationModel(
                this.PinPasswordBox.Password,
                this.PinRepeatPasswordBox.Password);
            UserRepository
                .ValidateRegistration(registrationModel)
                .Match(
                    Invalid: BadRegistration,
                    Valid: SuccessfulRegistration);
        }

        private void SuccessfulRegistration(RegistrationModel registrationModel)
        {
            UserRepository
                .SavePin(registrationModel)
                .Match(Exception: ShowErrorWhileSavingPin,
                    Success: _ => ShowSuccessfulSavePin());
        }

        private void ShowSuccessfulSavePin()
        {
            MessageBox.Show(
                this,
                "Registration successful!",
                "Information",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
            this.DialogResult = true;
            this.Close();
        }

        private void ShowErrorWhileSavingPin(Exception ex)
        {
            MessageBox.Show(
                this,
                ex.Message,
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }

        private void BadRegistration(IEnumerable<Error> errors)
        {
            var errorMessages = new StringBuilder("Registration was not successful. Error(s) encountered: \n\n");
            errors.ForEach(error => errorMessages.AppendLine(error.Message));
            MessageBox.Show(
                this, 
                errorMessages.ToString(), 
                "Error", 
                MessageBoxButton.OK, 
                MessageBoxImage.Error);
        }
    }
}
