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
