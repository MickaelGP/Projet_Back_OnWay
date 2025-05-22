using BackOnWay.Dtos.Visiteur;
using BackOnWay.Metier.Visiteur;
using BackOnWay.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackOnWay.Controllers.Visiteur
{
    [Route("[controller]")]
    [ApiController]
    public class InscriptionController : ControllerBase
    {
        private InscriptionMetier _metier = new InscriptionMetier();

        [HttpPost("/inscription")]
        public IActionResult CreateCompte([FromBody] CreateCompteDto unCompte)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(unCompte.UtilMdp, 12);
            unCompte.UtilMdp = passwordHash;

            int resultat = _metier.CreateCompte(unCompte);

            switch (resultat)
            {
                case 0:
                    return Conflict(new ProblemDetails
                    {
                        Title = "Email existant",
                        Detail = "L'inscritption n'est pas possible car l'adresse email existe déjà'",
                        Status = StatusCodes.Status409Conflict
                    });
                case 1:
                    return StatusCode(500, new ProblemDetails
                    {
                        Title = "Erreur interne",
                        Detail = "Une erreur est survenue lors de la création du compte",
                        Status = StatusCodes.Status500InternalServerError
                    });
                case 2:
                    new SendMailUtils().SendEmailnewCompteUtil(unCompte);
                    return Ok(new { message = "Inscription réussie" });
                default:
                    return StatusCode(500, new ProblemDetails
                    {
                        Title = "Erreur inconnue",
                        Detail = "Une erreur inattendue est survenue.",
                        Status = StatusCodes.Status500InternalServerError
                    });
            }

        }
    }
}
