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
                    Secure = false,
                    SameSite = SameSiteMode.Lax,
                    Expires = DateTimeOffset.UtcNow.AddHours(1)
                });

                return Ok(new { message = "Authentification réussie" });
            }
        }

        [HttpPost("/deconnexion")]
        public IActionResult Deconnexion()
        {
            string token = Request.Cookies["session_token"];
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Aucun token fourni.");
            }
            int resultat = _metier.Deconnexion(token);
            if (resultat == 0)
            {
                return StatusCode(500, "Une erreur s'est produite lors de la déconnexion !");
            }
            Response.Cookies.Delete("session_token");
            return Ok("Déconnexion réussie");

        }
    }
}
