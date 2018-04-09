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

namespace UserServerless
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
        public APIGatewayProxyResponse Get(APIGatewayProxyRequest request, ILambdaContext context)
        {
            string username = null;

            if (request.PathParameters.ContainsKey("username"))
            {
                username = request.PathParameters["username"].ToString();
            }

            if(!string.IsNullOrEmpty(username))
            {
                var user = UserRepository.Instance.RetrieveUser(username);

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

        public APIGatewayProxyResponse Post(APIGatewayProxyRequest request, ILambdaContext context)
        {
            Console.WriteLine("Post");
            Console.WriteLine(context);
            Console.WriteLine(request);

            var response = new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = "Update User Detail",
                Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
            };

            return response;
        }

        #endregion
    }
}
