using CoreApiClient.Entities;
using CoreApiClient.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyCards.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyCards.Controllers
{
    [Route("[controller]")]
    public class CardsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICardClient _cardsClient;

        public CardsController(ILogger<HomeController> logger, ICardClient cardsClient)
        {
            _logger = logger;
            _cardsClient = cardsClient;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            // return await _cardsClient.Get(user);
            return View();
        }

        [HttpPost("SaveCard")]
        public async Task<Message<Card>> SaveCard([FromBody] Card card)
        {
            var retorno = await _cardsClient.PostMessageAsync<Card>("SaveCard", card);
            return retorno;
        }

        [HttpGet]
        [Route("GetCards/{idDeckCards}")]
        public async Task<Message<List<Card>>> GetCards(int idDeckCards)
        {  
            return await _cardsClient.GetCards(idDeckCards);
        }

        [HttpPost("UpdateCard")]
        public async Task<Message<Card>> UpdateCard([FromBody] Card card)
        {
            var retorno = await _cardsClient.PostMessageAsync<Card>("UpdateCard", card);
            return retorno;
        }

    }
}
