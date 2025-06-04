using BackOnWay.Dtos.Auth;
using BackOnWay.Dtos.Utilisateur.Conducteur;
using BackOnWay.Metier.Utilisateur.Conducteur;
using BackOnWay.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackOnWay.Controllers.Utilisateur.Conducteur
{
    [Route("[controller]")]
    [ApiController]
    public class AjoutVoitureController : ControllerBase
    {
        private AjoutVoitureMetier _metier = new AjoutVoitureMetier();

        [HttpGet("/couleurs")]
        public IActionResult GetAllCouleurs()
        {
            List<Couleurs> listeCouleurs = _metier.GetAllCouleurs();

            if(listeCouleurs.Count < 1)
            {
                return NotFound();
            }

            return Ok(listeCouleurs);
        }

        [HttpGet("/modeles")]
        public IActionResult GetAllModeles()
        {
            List<Modeles> listeModeles = _metier.GetAllModeles();

            if( listeModeles.Count < 1)
            {
                return NotFound();
            }

            return Ok(listeModeles);
        }

        [HttpPost("/ajout-voitures")]
        public IActionResult InsertVoiture([FromBody] InsertVoitureDto unVoiture)
        {
            ConnexionInfoDto? util = HttpContext.Items["Utilisateur"] as ConnexionInfoDto;
            if (util == null)
            {
                return StatusCode(401, new ProblemDetails
                {
                    Title = "Token manquant",
                    Detail = "Utilisateur non connecté.",
                    Status = StatusCodes.Status401Unauthorized
                });
            }
            unVoiture.UtilId = util.UtilId;
            bool resultat = _metier.InsertVoiture(unVoiture);

            if (!resultat)
            {
                return StatusCode(500, new ProblemDetails
                {
                    Title = "Erreur lors de l'ajout",
                    Detail = "Une erreur s'est produite lors de l'ajout du véhicule !",
                    Status = StatusCodes.Status500InternalServerError
                });
            }

            return Ok(new { message = "Voiture ajoutée" });
        }
    }
}
