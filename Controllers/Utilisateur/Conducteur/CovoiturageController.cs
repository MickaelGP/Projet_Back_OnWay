using BackOnWay.Dtos.Auth;
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
        private readonly ICovoiturageMetier _metier;

        public CovoiturageController(ICovoiturageMetier metier)
        {
            _metier = metier;
        }

        [HttpDelete("/delete")]
        public IActionResult DeleteCovoitById([FromBody] int unId)
        {
            bool resultat = _metier.DeleteCovoitById(unId);

            if (!resultat)
            {
                return StatusCode(500, "Une erreur s'est produite lors de la suppression du covoiturage !");
            }

            return NoContent();
        }

        [HttpGet("/les-covoiturages")]
        public IActionResult GetAllCovoitByUtilId()
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
            int unId = util.UtilId;
            List<GetListeCovoitByUtilIdDto> listeCovoits = _metier.GetAllCovoitByUtilId(unId);

            return Ok(listeCovoits);
        }
    }
}
