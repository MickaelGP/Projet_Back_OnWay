using BackOnWay.Dtos.Utilisateur;
using BackOnWay.Metier.Utilisateur;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackOnWay.Controllers.Utilisateur
{
    [Route("[controller]")]
    [ApiController]
    public class AjoutVoitureController : ControllerBase
    {
        private AjoutVoitureMetier _metier = new AjoutVoitureMetier();

        [HttpPost]
        public IActionResult InsertVoiture([FromBody] InsertVoitureDto unVoiture)
        {
            bool resultat = _metier.InsertVoiture(unVoiture);

            if (!resultat)
            {
                return StatusCode(500, "Une erreur s'est produite lors de l'ajout du véhicule !");
            }

            return NoContent();
        }
    }
}
