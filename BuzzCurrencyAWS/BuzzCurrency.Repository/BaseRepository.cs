using Amazon;
using AWSSimpleClients.Clients;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuzzCurrency.Repository
{
    public class BaseRepository
    {
        #region Properties
        private RegionEndpoint _region { get; set; }
        private string _accessKey { get; set; }
        private string _secretKey { get; set; }
        #endregion

        protected BaseRepository()
        {
            _region = RegionEndpoint.GetBySystemName(Environment.GetEnvironmentVariable("region"));
            _accessKey = Environment.GetEnvironmentVariable("accessKey");
            _secretKey = Environment.GetEnvironmentVariable("secretKey");

            AWS.LoadAWSBasicCredentials(_region, _accessKey, _secretKey);
        }
    }
}
