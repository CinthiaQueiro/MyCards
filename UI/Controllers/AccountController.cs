﻿using CoreApiClient.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using CoreApiClient.Entities;


namespace MyCards.Controllers
{
    [AllowAnonymous, Route("account")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IUserClient _userClient;

        public AccountController(ILogger<AccountController> logger, IUserClient userClient)
        {
            _logger = logger;
            _userClient = userClient;
        }

        [Route("google-login")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [Route("google-response")]
        public async Task<Message<User>> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var clains = result.Principal.Identities
                .FirstOrDefault().Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });
            var name = clains.Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").FirstOrDefault().Value;
            var email = clains.Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").FirstOrDefault().Value;
            User user = new User {name = name, email = email};
            return await _userClient.PostAsync(user);
        }

        [HttpPost]
        [Route("SaveUser")]
        public async void SaveUser([FromBody] object user)
        {
            var teste = "oi";
           // return (await _contatoClient.ConsultaIdentificador(identificador, int.Parse(_user.OperacaoId))).Data;
        }

    }
}
