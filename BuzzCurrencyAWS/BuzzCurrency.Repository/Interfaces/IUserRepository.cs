using BuzzCurrency.Library.Models;

namespace BuzzCurrency.Repository.Interfaces
{
    interface IUserRepository
    {
        UserProfile RetrieveUser(string username);

        bool UpdateUser(UserProfile username);
    }
}
