using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.Core;
using AWSSimpleClients.Clients;
using BuzzCurrency.Logging;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace BuzzCurrency.UserPostConfirmation
{
    public class Function
    {
        #region Properties
        const string ConfirmSignUp = "PostConfirmation_ConfirmSignUp";
        const string EMPTY_STRING = "-";
        const string TableName = "BuzzCurrency-Users";

        private RegionEndpoint _region { get; set; }
        private string _accessKey { get; set; }
        private string _secretKey { get; set; }
        
        #endregion

        #region Constructor
        public Function()
        {
            _region = RegionEndpoint.GetBySystemName(Environment.GetEnvironmentVariable("region"));
            _accessKey = Environment.GetEnvironmentVariable("accessKey");
            _secretKey = Environment.GetEnvironmentVariable("secretKey");
        }

        public Function(RegionEndpoint region, string accessKey, string secretKey)
        {
            _region = region;
            _accessKey = accessKey;
            _secretKey = secretKey;
        }
        #endregion

        #region Function
        /// <summary>
        /// create a user record into dynamo db which user is confirmed
        /// </summary>
        /// <param name="context"></param>
        public CognitoContext FunctionHandler (CognitoContext model, ILambdaContext context)
        {
            AWS.LoadAWSBasicCredentials(_region, _accessKey, _secretKey);

            Console.WriteLine(JsonConvert.SerializeObject(model));

            if (model.TriggerSource == ConfirmSignUp)
            {
                try
                {
                    UserAttributes attributes = model.Request.UserAttributes;
                    Dictionary<string, AttributeValue> userAttributes = new Dictionary<string, AttributeValue>
                    {
                        ["Email"] = new AttributeValue() { S = attributes.CognitoEmail_Alias },
                        ["EmailVerified"] = new AttributeValue() { BOOL = attributes.CognitoUser_Status == "CONFIRMED" ? true : false },
                        ["Name"] = new AttributeValue() { S = attributes.Name },
                        ["PhoneNumber"] = new AttributeValue() { S = attributes.Phone_Number },
                        ["PhoneNumberVerified"] = new AttributeValue() { BOOL = attributes.Phone_Number_Verified == "true" ? true : false },
                        ["Birthdate"] = new AttributeValue() { S = attributes.Birthdate.ToString() },
                        ["Gender"] = new AttributeValue() { S = EMPTY_STRING },
                        ["Address"] = new AttributeValue() { S = EMPTY_STRING },
                        ["Country"] = new AttributeValue() { S = EMPTY_STRING }
                    };

                    var response = AWS.DynamoDB.PutItemAsync(new PutItemRequest()
                    {
                        TableName = TableName,
                        Item = userAttributes
                    }).GetAwaiter().GetResult();

                    if (response.HttpStatusCode != System.Net.HttpStatusCode.OK && 
                        response.HttpStatusCode != System.Net.HttpStatusCode.Accepted)
                    {
                        throw new Exception(string.Format("Failed to create confirmed user - {0}", userAttributes["Email"].S.ToString()));
                    }
                }
                catch (AmazonDynamoDBException exception)
                {
                    Logger.Instance.LogException(exception);
                }
                catch (Exception exception)
                {
                    Logger.Instance.LogException(exception);
                }
            }

            return model;
        }
        #endregion
    }
}
