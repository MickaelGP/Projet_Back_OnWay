using BackOnWay.Dtos.Auth;
using BackOnWay.Dtos.Utilisateur.Conducteur;
using BackOnWay.Metier.Utilisateur.Conducteur;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackOnWay.Controllers.Utilisateur.Conducteur
{
    [Route("[controller]")]
    [ApiController]
    public class UpdateCovoitController : ControllerBase
    {
        private readonly IUpdateCovoitMetier _metier;

        public UpdateCovoitController(IUpdateCovoitMetier metier)
        {
            _metier = metier;
        }
        [HttpGet("/update-covoiturage/{unId}")]
        public IActionResult GetInfoCovoitById(int unId)
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
            UpdateCovoitDto unCovoit = _metier.GetInfoCovoitById(unId);
            if (unCovoit == null) {
                return StatusCode(404, new ProblemDetails
                {
                    Detail = "Covoiturage non trouvé",
                    Title = "Aucun covoiturage n'a été trouvé",
                    Status = StatusCodes.Status404NotFound
                });
            }
            return Ok(unCovoit);
        }


        [HttpPut("/update-trajet")]
        public IActionResult UpdateCovoit([FromBody] UpdateCovoitDto updateCovoit)
        {
            int reponse = _metier.UpdateCovoit(updateCovoit);

            switch (reponse)
            {
                case 1:
                    // 409 Conflict : Covoiturage non terminé
                    return StatusCode(409, new ProblemDetails
                    {
                        Title = "Covoiturage terminé",
                        Detail = "La modification du covoiturage n'est pas possible car le covoiturage est terminé",
                        Status = StatusCodes.Status409Conflict
                    });
                case 2:
                    // 200
                    return Ok(new {message = "Trajet modifié"});
                case 3:
                    // 500 Internal Server Error : erreur technique
                    return StatusCode(500, new ProblemDetails
                    {
                        Title = "Erreur interne",
                        Detail = "Une erreur est survenue lors de l'ajout de la réservation.",
                        Status = StatusCodes.Status500InternalServerError
                    });
                default:
                    // 500 Internal Server Error : Erreur inconnue
                    return StatusCode(500, new ProblemDetails
                    {
                        Title = "Erreur inconnue",
                        Detail = "Une erreur inattendue est survenue.",
                        Status = StatusCodes.Status500InternalServerError
                    });
            }
        }
    }
}
