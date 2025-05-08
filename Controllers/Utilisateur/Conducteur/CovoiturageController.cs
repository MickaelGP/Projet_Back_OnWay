using BackOnWay.Dtos.Utilisateur.Conducteur;
using BackOnWay.Metier.Utilisateur.Conducteur;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackOnWay.Controllers.Utilisateur.Conducteur
{
    [Route("[controller]")]
    [ApiController]
    public class CovoiturageController : ControllerBase
    {
        private readonly  ICovoiturageMetier _metier;

        public CovoiturageController(ICovoiturageMetier metier)
        {
            _metier = metier;
        }

        [HttpGet("/les-covoiturages")]
        public IActionResult GetAllCovoitByUtilId(int unId)
        {
            List<GetListeCovoitByUtilIdDto> listeCovoits = _metier.GetAllCovoitByUtilId(unId);

            if(listeCovoits.Count == 0)
            {
                return NotFound();
            }

            return Ok(listeCovoits);
        }
    }
}
