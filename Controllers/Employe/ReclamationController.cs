using BackOnWay.Dtos.Employe;
using BackOnWay.Metier.Employe;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackOnWay.Controllers.Employe
{
    [Route("[controller]")]
    [ApiController]
    public class ReclamationController : ControllerBase
    {
        private ReclamationMetier _metier = new ReclamationMetier();

        [HttpGet]
        public IActionResult GetAllPlaintes()
        {
            List<PlaintesDto> listePlainte = _metier.GetAllPlaintes();

            return Ok(listePlainte);
        }

        [HttpGet("info-plainte/{id}")]
        public IActionResult GetPlainteById(int id)
        {
            PlainteByIdDto unePlainte = _metier.GetPlainteById(id);
            if (unePlainte != null)
            {
                return Ok(unePlainte);
            }
            else
            {
                return NotFound("Plainte non trouvée !");
            }
        }

        [HttpPut("update")]
        public IActionResult UpadteStatutPlainte([FromBody] PlainteUpdateDto unePlainte)
        {
            if (unePlainte == null)
            {
                return BadRequest("L'élément n'est pas valide");
            }
            bool modif = _metier.UpdateStatutPlainte(unePlainte);

            if (!modif)
            {
                return StatusCode(500, "Une erreur s'est produite lors de la mise à jour du statut de la plainte.");
            }

            return NoContent();

        }
    }
}
