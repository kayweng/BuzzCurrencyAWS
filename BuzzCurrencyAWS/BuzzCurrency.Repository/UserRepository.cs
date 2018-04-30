using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using BuzzCurrency.Library.Models;
using BuzzCurrency.Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace BuzzCurrency.Repository
{

    public class UserRepository : BaseRepository, IUserRepository
    {
        IDynamoDBContext DDBContext { get; set; }
        
        public UserRepository(string tableName)
        {
            var config = new DynamoDBContextConfig { Conversion = DynamoDBEntryConversion.V2 };

            AWSConfigsDynamoDB.Context.TypeMappings[typeof(UserProfile)] = new Amazon.Util.TypeMapping(typeof(UserProfile), tableName);

            DDBContext = new DynamoDBContext(new AmazonDynamoDBClient(), config);
        }

        public async Task<UserProfile> RetrieveUser(string username)
        {
            try
            {
                var user = await DDBContext.LoadAsync<UserProfile>(username);

                return user;
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
                await DDBContext.SaveAsync(userProfile).ContinueWith(task =>
                {
                    return (task.Exception == null);
                });

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
