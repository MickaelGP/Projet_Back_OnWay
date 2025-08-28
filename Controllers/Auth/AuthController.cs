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
            if (session == null || !string.IsNullOrEmpty(session.MessageErreur))
            {
                switch (session?.MessageErreur)
                {
                    case "Utilisateur introuvable":
                        return NotFound(new ProblemDetails
                        {
                            Title = "Utilisateur introuvable",
                            Detail = "Aucun compte n'existe.",
                            Status = StatusCodes.Status404NotFound
                        });
                    case "Vôtre compte à été suspendu":
                        return Unauthorized(new ProblemDetails
                        {
                            Title = "Compte suspendu",
                            Detail = "Vôtre compte à été suspendu",
                            Status = StatusCodes.Status401Unauthorized
                        });
                    case "Mot de passe incorrect":
                        return Unauthorized(new ProblemDetails
                        {
                            Title = "Mot de passe incorrect",
                            Detail = "Email ou mot de passe incorect",
                            Status = StatusCodes.Status401Unauthorized
                        });

                    case "Erreur de session":
                        return StatusCode(500, new ProblemDetails
                        {
                            Title = "Erreur de session",
                            Detail = "La session n’a pas pu être créée.",
                            Status = StatusCodes.Status500InternalServerError
                        });
                    default:
                        return StatusCode(500, new ProblemDetails
                        {
                            Title = "Erreur inconnue",
                            Detail = "une erreur inconnue est survenue.",
                            Status = StatusCodes.Status500InternalServerError
                        });
                }
            }
            else
            {
                Response.Cookies.Append("session_token", session.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Lax,
                    Expires = DateTimeOffset.UtcNow.AddHours(1),
                    Path= "/"
                });

                return Ok(new
                {
                    message = "Authentification réussie",
                    utilisateur = new {session.RoleLabel }
                });
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
                return StatusCode(500, new ProblemDetails
                {
                    Title = "Erreur inconnue",
                    Detail = "une erreur inconnue est survenue.",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Path = "/"
            };

            Response.Cookies.Delete("session_token", cookieOptions);
            return Ok("Déconnexion réussie");

        }
    }
}
