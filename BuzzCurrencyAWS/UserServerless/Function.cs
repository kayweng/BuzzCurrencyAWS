using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Amazon;
using AWSSimpleClients.Clients;
using BuzzCurrency.Library.Consts;
using Amazon.DynamoDBv2.Model;
using Newtonsoft.Json;
using BuzzCurrency.Repository;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace BuzzCurrency.Serverless.User
{
    public class Functions
    {
        #region Properties
        private RegionEndpoint _region { get; set; }
        private string _accessKey { get; set; }
        private string _secretKey { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor that Lambda will invoke.
        /// </summary>
        public Functions()
        {
            _region = RegionEndpoint.GetBySystemName(Environment.GetEnvironmentVariable("region"));
            _accessKey = Environment.GetEnvironmentVariable("accessKey");
            _secretKey = Environment.GetEnvironmentVariable("secretKey");

            AWS.LoadAWSBasicCredentials(_region, _accessKey, _secretKey);
        }
        #endregion

        #region API Methods
        /// <summary>
        /// Retrieve buzz currency user async
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<APIGatewayProxyResponse> GetUserAsync(APIGatewayProxyRequest request, ILambdaContext context)
        {
            string username = null;

            if (request.PathParameters.ContainsKey("username"))
            {
                username = request.PathParameters["username"].ToString();
            }

            if(!string.IsNullOrEmpty(username))
            {
                var user = await UserRepository.Instance.RetrieveUser(username);

                if (user != null)
                {
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Body = JsonConvert.SerializeObject(user),
                        Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                    };
                }
            }

            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.NotFound
            };
        }
        #endregion
    }
}
