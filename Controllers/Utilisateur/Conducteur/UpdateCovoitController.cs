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

        [HttpPut]
        public IActionResult UpdateCovoit([FromBody] UpdateCovoitDto updateCovoit)
        {
            int reponse = _metier.UpdateCovoit(updateCovoit);

            switch (reponse)
            {
                case 1:
                    // 409 Conflict : Covoiturage non terminé
                    return Conflict(new ProblemDetails
                    {
                        Title = "Covoiturage en cours",
                        Detail = "La modification du covoiturage n'est pas possible car le covoiturage est en cours",
                        Status = StatusCodes.Status409Conflict
                    });
                case 2:
                    // 204 Created : succès 
                    return Created();
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
