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
    [Authorize]
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

    }
}
