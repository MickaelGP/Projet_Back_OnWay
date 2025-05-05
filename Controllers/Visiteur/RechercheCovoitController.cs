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

        [HttpPost]
        public IActionResult GetExactCovoiturages([FromBody] RechercheCovoitDto recherche)
        {
            RechercheCovoitDto unCovoit = _metier.GetExactCovoiturages(recherche);
            if (unCovoit == null)
            {
                return NotFound("Aucun résultat trouvé !");
            }
            return Ok(unCovoit);
        }
    }
}
