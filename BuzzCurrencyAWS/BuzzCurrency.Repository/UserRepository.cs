using Amazon.DynamoDBv2.DocumentModel;
using AWSSimpleClients.Clients;
using BuzzCurrency.Library.Consts;
using BuzzCurrency.Library.Enums;
using BuzzCurrency.Library.Helpers;
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
                    var user = new UserProfile();

                    if (document.ContainsKey("Email"))
                    {
                        user.Email = document["Email"].AsString();
                    }

                    if (document.ContainsKey("EmailVerified"))
                    {
                        user.EmailVerified = document["EmailVerified"].AsBoolean();
                    }

                    if (document.ContainsKey("FirstName"))
                    {
                        user.FirstName = document["FirstName"].AsString();
                    }

                    if (document.ContainsKey("LastName"))
                    {
                        user.LastName = document["LastName"].AsString();
                    }

                    if (document.ContainsKey("Birthdate"))
                    {
                        user.Birthdate = document["Birthdate"].AsString();
                    }

                    if (document.ContainsKey("PhoneNumber"))
                    {
                        user.PhoneNumber = document["PhoneNumber"].AsString();
                    }

                    if (document.ContainsKey("PhoneNumberVerified"))
                    {
                        user.PhoneNumberVerified = document["PhoneNumberVerified"].AsBoolean();
                    }

                    if (document.ContainsKey("Gender"))
                    {
                        user.Gender = document["Gender"].AsString();
                    }

                    if (document.ContainsKey("Address"))
                    {
                        user.Address = document["Address"].AsString();
                    }

                    if (document.ContainsKey("Country"))
                    {
                        user.Country = document["Country"].AsString();
                    }

                    if (document.ContainsKey("UserType"))
                    {
                        user.UserType = (UserType)Enum.Parse(typeof(UserType), document["UserType"].AsString());
                        user.UserTypeDescription = EnumHelper.GetDescription<UserType>(user.UserType).ToString();
                    }

                    if (document.ContainsKey("ImageUrl"))
                    {
                        user.ImageUrl = document["ImageUrl"].AsString();
                    }

                    if (document.ContainsKey("Active"))
                    {
                        user.Active = document["Active"].AsBoolean();
                    }

                    if (document.ContainsKey("CreatedOn"))
                    {
                        user.CreatedOn = document["CreatedOn"].AsString();
                    }

                    if (document.ContainsKey("ModifiedOn"))
                    {
                        user.ModifiedOn = document["ModifiedOn"].AsString();
                    }

                    return user;
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
