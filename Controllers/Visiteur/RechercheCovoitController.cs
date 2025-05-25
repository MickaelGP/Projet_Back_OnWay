using BackOnWay.Dtos.Visiteur;
using BackOnWay.Metier.Visiteur;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackOnWay.Controllers.Visiteur
{
    [Route("[controller]")]
    [ApiController]
    public class RechercheCovoitController : ControllerBase
    {
        private RechercheCovoitMetier _metier = new RechercheCovoitMetier();

        [HttpPost("/recherche-covoiturage")]
        public IActionResult GetExactCovoiturages([FromBody] RechercheCovoitDto recherche)
        {
            List<RechercheCovoitDto> unCovoit = _metier.GetExactCovoiturages(recherche);
            if (unCovoit.Count < 1)
            {
                return StatusCode(404, new ProblemDetails
                {
                    Title = "Aucun covoiturages",
                    Detail = "Aucun covoiturage n'a été trouvé",
                    Status = StatusCodes.Status404NotFound
                });
            }
            return Ok(unCovoit);
        }
    }
}
