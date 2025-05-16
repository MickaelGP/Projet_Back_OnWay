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
            ConnexionInfoDto? session = _metier.Authentifier(uneConnexion);
            if (session == null)
            {
                return NotFound();
            }
            else
            {
                Response.Cookies.Append("session_token", session.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddHours(1)
                });

                return Ok(session);
            }
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
