using Subscription.Domain;

namespace Subscription.ViewModels
{
    public class AppModel
    {
        public bool IsUserRegistered()
        {
            return UserRepository.IsUserRegistered();
        }
    }
}