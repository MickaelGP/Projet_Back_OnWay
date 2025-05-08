using BackOnWay.Dtos.Utilisateur;
using BackOnWay.Metier.Utilisateur.Conducteur;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackOnWay.Controllers.Utilisateur.Conducteur
{
    [Route("[controller]")]
    [ApiController]
    public class AjoutCovoiturageController : ControllerBase
    {
        private AjoutCovoiturageMetier _metier = new AjoutCovoiturageMetier();


        [HttpGet("/liste-voitures/{unId}")]
        public IActionResult GetUtilVoitures(int unId)
        {
            List<GetListeVoitureDto> listeVoitures = _metier.GetUtilVoitures(unId);

            return Ok(listeVoitures);
        }

        [HttpPost]
        public IActionResult InsertCovoiturage([FromBody] InsertCovoiturageDto unCovoiturage)
        {
            int reponse = _metier.InsertCovoiturage(unCovoiturage);

            switch (reponse)
            {
                case 1:
                    return Conflict("Vous avez déjà un covoiturage de prévu à cette date");
                case 2:
                    return Conflict("Votre solde de crédit est insuffisant");
                case 3:
                    return NoContent();
                case 4:
                    return StatusCode(500, "Une erreur s'est produite lors de la création du covoiturage !");
                default:
                    return StatusCode(520, "Une erreur inconnue est survenue.");
            }
        }
    }
}
