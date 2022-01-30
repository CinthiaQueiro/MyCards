using CoreApiClient.Entities;
using CoreApiClient.Interfaces;
using CoreApiClient.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyCards.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyCards.Controllers
{
    [Authorize]
    public class DeckCardsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _session;
        private readonly IDeckCardClient _deckCard;
        public DeckCardsController(ILogger<HomeController> logger, IHttpContextAccessor session, IDeckCardClient deckCard)
        {
            _logger = logger;
            _session = session;
            _deckCard = deckCard;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            //recover user 
            SessionUtility session = new SessionUtility(_session);
            User user = JsonConvert.DeserializeObject<User>(session.GetSession("user"));
            var decks = await _deckCard.GetDeckCardsUser(user.Id);
            return View(decks);
        }

    }
}
