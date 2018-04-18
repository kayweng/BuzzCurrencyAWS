using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using BuzzCurrency.Library.Consts;
using BuzzCurrency.Library.Models;
using BuzzCurrency.Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace BuzzCurrency.Repository
{

    public class UserRepository : BaseRepository, IUserRepository
    {
        public static UserRepository _instance = new UserRepository();
        IDynamoDBContext DDBContext { get; set; }
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
            var config = new DynamoDBContextConfig { Conversion = DynamoDBEntryConversion.V2 };

            AWSConfigsDynamoDB.Context.TypeMappings[typeof(UserProfile)] = new Amazon.Util.TypeMapping(typeof(UserProfile), DynamoTables.Users);

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

        public bool UpdateUser(UserProfile username)
        {
            return true;
        }
    }
}
