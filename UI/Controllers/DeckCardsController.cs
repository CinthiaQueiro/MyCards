using AutoMapper;
using CoreApiClient.Entities;
using CoreApiClient.Interfaces;
using CoreApiClient.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI.ViewModel;

namespace MyCards.Controllers
{
    [Route("[controller]")]
    public class DeckCardsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _session;
        private readonly IDeckCardClient _deckCard;
        private readonly IMapper _mapper;
        public DeckCardsController(ILogger<HomeController> logger, 
            IHttpContextAccessor session, 
            IDeckCardClient deckCard,
            IMapper mapper)
        {
            _logger = logger;
            _session = session;
            _deckCard = deckCard;
            _mapper = mapper;
        }

       

        [HttpGet]
        [Route("GetDeckCards")]

        public async Task<List<DeckCard>> GetDeckCards()
        {   //recover user 
            SessionUtility session = new SessionUtility(_session);
            var sessao = session.GetSession("user");
            List<DeckCard> decks = new List<DeckCard>();
            if (session.GetSession("user") == null)
            {
                RedirectToAction("google-login", "Account");
            } else
            {
                User user = JsonConvert.DeserializeObject<User>(session.GetSession("user"));
                decks = (await _deckCard.GetDeckCardsUser(user.Id)).Data;
            }
                   
            return decks;
        }

        [HttpPost("SaveDeck")]
        public async Task<Message<DeckCard>> SaveDeck([FromBody] DeckCard deckCard)
        {
            var retorno = await _deckCard.PostMessageAsync<DeckCard>("SaveDeck", deckCard);
            return retorno;
        }

        [HttpPost("EditDeckCard")]
        public async Task<Message<DeckCard>> EditDeckCard([FromBody] DeckCard deckCard)
        {
            var retorno = await _deckCard.PostMessageAsync<DeckCard>("EditDeckCard", deckCard);
            return retorno;
        }

        [HttpPost("DeleteDeckCard")]
        public async Task<Message<DeckCard>> DeleteDeckCard([FromBody] DeckCard deckCard)
        {
            var retorno = await _deckCard.PostMessageAsync<DeckCard>("DeleteDeckCard", deckCard);
            return retorno;
        }

    }
}
