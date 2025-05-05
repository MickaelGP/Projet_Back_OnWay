using BackOnWay.Dtos.Employe;
using BackOnWay.Metier.Employe;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackOnWay.Controllers.Employe
{
    [Route("[controller]")]
    [ApiController]
    public class ValideAvisController : ControllerBase
    {
        private ValidAvisMetier _metier = new ValidAvisMetier();

        [HttpGet]
        public IActionResult GetAllAvis()
        {
            List<AvisDto> listeAvis = _metier.GetAllAvis();

            return Ok(listeAvis);
        }

        [HttpGet("info-avis/{unId}")]
        public IActionResult GetAvisById(int unId)
        {
            AvisByIdDto unAvis = _metier.GetAvisById(unId);
            if (unAvis == null)
            {
                return NotFound("Avis non trouvée !");
            }

            return Ok(unAvis);
        }

        [HttpPut]
        public IActionResult UpdateStatutAvis([FromBody] AvisUpdateDto unAvis)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool modif = _metier.UpdateStatutAvis(unAvis);

            if (!modif)
            {
                return StatusCode(500, "Une erreur c'est produite lors de la mise à jour du statut de l'avis.");
            }

            return NoContent();
        }
    }
}
