using BackOnWay.Dtos.Auth;
using BackOnWay.Metier.Auth;
using BackOnWay.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackOnWay.Controllers.Auth
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthMetier _metier;

        public AuthController(IAuthMetier metier)
        {
            _metier = metier;
        }

        [HttpPost("/connexion")]
        public IActionResult Authentifier([FromBody] ConnexionDto uneConnexion)
        {
            ResultatAuthDto unUtil = _metier.Connexion(uneConnexion);

            return new JsonResult(unUtil);
            //int resultat = _metier.Authentifier(uneConnexion);

            //switch (resultat)
            //{
            //    case 0:
            //        return BadRequest("Les information de connexion ne sont pas correcte");
            //    case 1:
            //        return NoContent();
            //    default:
            //        return StatusCode(520, "Une erreur inconnue est survenue.");
            //}
        }
    }
}
