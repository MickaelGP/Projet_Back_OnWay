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

        [HttpPost]
        public IActionResult CreateCompte([FromBody] CreateCompteDto unCompte)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(unCompte.UtilMdp, 12);
            unCompte.UtilMdp = passwordHash;

            int resultat = _metier.CreateCompte(unCompte);

            switch (resultat)
            {
                case 0:
                    return Conflict("L'email existe déjà.");
                case 1:
                    return StatusCode(500, "Une erreur s'est produite lors de la création du compte !");
                case 2:
                    new SendMailUtils().SendEmailnewCompteUtil(unCompte);
                    return NoContent();
                default:
                    return StatusCode(520, "Une erreur inconnue est survenue.");
            }

        }
    }
}
