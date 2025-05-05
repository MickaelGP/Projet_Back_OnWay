using BackOnWay.Dtos.Visiteur;
using BackOnWay.Metier.Visiteur;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackOnWay.Controllers.Visiteur
{
    [Route("[controller]")]
    [ApiController]
    public class DetailsCovoitController : ControllerBase
    {
        private DetailsCovoitMetier _metier = new DetailsCovoitMetier();

        [HttpGet("{unId}")]
        public IActionResult GetInfoCovoit(int unId)
        {
            DetailsCovoitDto infoCovoit = _metier.GetInfoCovoit(unId);

            if (infoCovoit == null)
            {
                return NotFound();
            }

            return Ok(infoCovoit);
        }
        [HttpGet("/avis/{unId}")]
        public IActionResult GetAvisByConducId(int unId)
        {
            List<AvisRecuDto> listeAvis = _metier.GetAvisByConducId(unId);

            if (listeAvis.Count < 1)
            {
                return NotFound();
            }

            return Ok(listeAvis);
        }
    }
}
