﻿using BuzzCurrency.Library.Models;
using System.Threading.Tasks;

namespace BuzzCurrency.Repository.Interfaces
{
    interface IUserRepository
    {
        Task<UserProfile> RetrieveUser(string username);

        Task<bool> SaveUser(UserProfile userProfile);
    }
}
