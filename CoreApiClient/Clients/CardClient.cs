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
    public class CardClient : ApiClientBase<Card>, ICardClient
    {
        public override string _url { get; set; } = "api/Card/";

        public CardClient(IConfiguration configuration, SessionUtility sessionUtility) : base(configuration, sessionUtility)
        {
        }

        public async Task<Message<User>> Get(string email)
        {
            return await GetMessageAsync<User>($"Get/{email}");
        }

        public async Task<Message<List<Card>>> GetCards(int idDeckCards)
        {
            return await GetMessageAsync<List<Card>>($"GetCards/{idDeckCards}");
        }
    }
}
