using BackOnWay.Dtos.Auth;
using BackOnWay.Metier.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackOnWay.Controllers.Auth
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private AuthMetier _metier = new AuthMetier();

        [HttpPost("/connexion")]
        public IActionResult Authentifier([FromBody] ConnexionDto uneConnexion)
        {
            int resultat = _metier.Authentifier(uneConnexion);

            switch (resultat)
            {
                case 0:
                    return BadRequest("Les information de connexion ne sont pas correcte");
                case 1:
                    return NoContent();
                default:
                    return StatusCode(520, "Une erreur inconnue est survenue.");
            }
        }
    }
}
