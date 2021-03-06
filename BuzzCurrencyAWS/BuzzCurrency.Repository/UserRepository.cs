﻿using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using BuzzCurrency.Library.Models;
using BuzzCurrency.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AWSSimpleClients.Clients;
using BuzzCurrency.Library.Helpers;
using BuzzCurrency.Library.Enums;

namespace BuzzCurrency.Repository
{

    public class UserRepository : BaseRepository, IUserRepository
    {
        IDynamoDBContext DDBContext { get; set; }
        string userTableName { get; set; }

        public UserRepository(string tableName)
        {
            var config = new DynamoDBContextConfig { Conversion = DynamoDBEntryConversion.V2 };

            AWSConfigsDynamoDB.Context.TypeMappings[typeof(UserProfile)] = new Amazon.Util.TypeMapping(typeof(UserProfile), tableName);

            DDBContext = new DynamoDBContext(new AmazonDynamoDBClient(), config);

            userTableName = tableName;
        }

        public async Task<UserProfile> RetrieveUser(string username)
        {
            try
            {
                var user = await DDBContext.LoadAsync<UserProfile>(username);
                user.UserTypeDescription = EnumHelper.GetDescription<UserType>(user.UserType);

                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }

        public async Task<bool> UpdateUser(UserProfile userProfile) 
        {
            try
            {
                var request = new UpdateItemRequest
                {
                    TableName = userTableName,
                    Key = new Dictionary<string, AttributeValue>() 
                    {
                        { "Email", new AttributeValue { S = userProfile.Email } } 
                    },
                    ExpressionAttributeNames = new Dictionary<string, string>()
                    {
                        {"#FirstName", "FirstName"},
                        {"#LastName", "LastName"},
                        {"#Mobile", "Mobile"},
                        {"#Birthdate", "Birthdate"},
                        {"#Gender", "Gender"},
                        {"#Address", "Address"},
                        {"#Country", "Country"},
                        {"#ImageUrl", "ImageUrl"},
                        {"#ModifiedOn", "ModifiedOn"}
                    },
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                    {
                        {":firstName", new AttributeValue { S = userProfile.FirstName }},
                        {":lastName", new AttributeValue { S = userProfile.LastName }},
                        {":mobile", new AttributeValue { S = userProfile.Mobile }},
                        {":birthdate", new AttributeValue { S = userProfile.Birthdate }},
                        {":gender", new AttributeValue { S = userProfile.Gender }},
                        {":address", new AttributeValue { S = userProfile.Address }},
                        {":country", new AttributeValue { S = userProfile.Country }},
                        {":imageUrl", new AttributeValue { S = userProfile.ImageUrl }},
                        {":modifiedOn", new AttributeValue { S = userProfile.ModifiedOn }}
                    },

                    UpdateExpression = "SET #FirstName = :firstName, #LastName = :lastName, #Mobile = :mobile, #Birthdate = :birthdate, #Gender = :gender, #Address = :address, #Country = :country, #ImageUrl = :imageUrl, #ModifiedOn = :modifiedOn"
                };

                return await AWS.DynamoDB.UpdateItemAsync(request).ContinueWith(task =>
                {
                    if (task.IsFaulted){
                        Console.WriteLine(task.Exception.Message);   
                    }

                    return !task.IsFaulted;
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> SaveUser(UserProfile userProfile)
        {
            try
            {
                return await DDBContext.SaveAsync(userProfile).ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        Console.WriteLine(task.Exception.Message);
                    }

                    return !task.IsFaulted;
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
