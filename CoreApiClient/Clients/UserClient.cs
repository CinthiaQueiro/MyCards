using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CoreApiClient.Interfaces;
using CoreApiClient.Utility;
using CoreApiClient.Entities;
using Microsoft.Extensions.Configuration;

namespace CoreApiClient.Clients
{
    public class UserClient : ApiClientBase<User>, IUserClient
    {
        public override string _url { get; set; } = "api/User/";

        public UserClient(IConfiguration configuration, SessionUtility sessionUtility) : base(configuration, sessionUtility)
        {
        }

        public async Task<Message<User>> Get(string email)
        {
            return await GetMessageAsync<User>($"Get/{email}");
        }
    }
}
