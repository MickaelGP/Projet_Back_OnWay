using BackOnWay.Dtos.Utilisateur.Passager;
using BackOnWay.Metier.Utilisateur.Passager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackOnWay.Controllers.Utilisateur.Passager
{
    [Route("[controller]")]
    [ApiController]
    public class HistoriqueCovoitController : ControllerBase
    {
        private readonly IHistoriqueCovoitMetier _metier;

        public HistoriqueCovoitController(IHistoriqueCovoitMetier metier)
        {
            _metier = metier;
        }

        [HttpGet("{unId}")]
        public IActionResult GetAllCovoitByUtilId(int unId)
        {
            List<HistoriqueCovoitDto> listeCovoits = _metier.GetAllCovoitByUtilId(unId);

            if (listeCovoits.Count == 0)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Aucun covoiturage trouvé",
                    Detail = "Aucun historique de covoiturage n’a été trouvé.",
                    Status = StatusCodes.Status404NotFound,
                });
            }

            return Ok(listeCovoits);
        }
    }
}
