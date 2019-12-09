using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using LaYumba.Functional;
using static Subscription.Configuration.SettingManager;
using static LaYumba.Functional.F;
using Unit = System.ValueTuple;

namespace Subscription.Domain
{
    public class UserRepository
    {
        private static readonly Regex PinRegex = new Regex("^.{4,12}$");
        private static readonly string DataFolder = AppSettings.DataFolder;
        private static readonly string SettingsDirectory = Path.Combine(DataFolder, "settings");
        private static readonly string UserFilePath = Path.Combine(SettingsDirectory, "user");

        public static bool IsUserRegistered()
        {
            return File.Exists(UserFilePath);
        }

        public static Validation<RegistrationModel> ValidateRegistration(RegistrationModel registrationModel)
        {
            if (!PinRegex.IsMatch(registrationModel.Pin))
                return Errors.PinRulesViolation;

            if (registrationModel.Pin != registrationModel.PinRepeat)
                return Errors.PinsDoNotMatch;

            return registrationModel;
        }

        public static Validation<Unit> ValidateLogin(string pin)
        {
            var encPin = File.ReadAllText(UserFilePath);
            var key = GetPinHash(pin);
            if (encPin == key)
            {
                return Unit();
            }

            return Errors.PinIncorrect;
        }

        public static Exceptional<Unit> SavePin(RegistrationModel registrationModel)
        {
            try
            {
                Directory.CreateDirectory(SettingsDirectory);
                var key = GetPinHash(registrationModel.Pin);
                File.WriteAllText(UserFilePath,key);
            }
            catch (Exception ex)
            {
                return ex;
            }

            return Unit();
        }

        private static string GetPinHash(string pin) => Hash(Encoding.UTF8.GetBytes(pin));

        private static string Hash(byte[] input)
        {
            using var hashAlgorithm = HashAlgorithm.Create("SHA256");
            return Convert.ToBase64String(hashAlgorithm.ComputeHash(input));
        }
    }
}