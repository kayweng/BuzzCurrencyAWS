using Amazon.DynamoDBv2.DocumentModel;
using AWSSimpleClients.Clients;
using BuzzCurrency.Library.Consts;
using BuzzCurrency.Library.Enums;
using BuzzCurrency.Library.Models;
using BuzzCurrency.Repository.Interfaces;
using System;

namespace BuzzCurrency.Repository
{

    public class UserRepository : BaseRepository, IUserRepository
    {
        public static UserRepository _instance = new UserRepository();
        Table table;

        public static UserRepository Instance
        {
            get
            {
                return _instance;
            }
        }

        public UserRepository()
        {
            table = Table.LoadTable(AWS.DynamoDB, DynamoTables.Users);
        }

        public UserProfile RetrieveUser(string username)
        {
            try
            {
                Document document = table.GetItemAsync(username).GetAwaiter().GetResult();

                if (document != null)
                {
                    return new UserProfile()
                    {
                        Email = document["Email"].AsString(),
                        EmailVerified = document["EmailVerified"].AsBoolean(),
                        Name = document["Name"].AsString(),
                        Birthdate = document["Birthdate"].AsString(),
                        PhoneNumber = document["PhoneNumber"].AsString(),
                        PhoneNumberVerified = document["PhoneNumberVerified"].AsBoolean(),
                        Gender = document["Gender"].AsString(),
                        Address = document["Address"].AsString(),
                        Country = document["Gender"].AsString(),
                        UserType = (UserType)Enum.Parse(typeof(UserType), document["UserType"].AsString()),
                        ImageUrl = document["ImageUrl"].AsString(),
                        Active = document["Active"].AsBoolean(),
                        CreatedOn = document["CreatedOn"].AsString(),
                        ModifiedOn = document["ModifiedOn"].AsString()
                    };
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }  
            
            return null;
        }

        public bool UpdateUser(UserProfile username)
        {
            return true;
        }
    }
}
