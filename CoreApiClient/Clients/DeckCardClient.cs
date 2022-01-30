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
    public class DeckCardClient : ApiClientBase<DeckCard>, IDeckCardClient
    {
        public override string _url { get; set; } = "api/DeckCards/";

        public DeckCardClient(IConfiguration configuration, SessionUtility sessionUtility) : base(configuration, sessionUtility)
        {
        }

        public async Task<Message<List<DeckCard>>> GetDeckCardsUser(int idUser)
        {
            return await GetMessageAsync<List<DeckCard>>($"GetDeckCardsUser/{idUser}");

        }

    }
}
