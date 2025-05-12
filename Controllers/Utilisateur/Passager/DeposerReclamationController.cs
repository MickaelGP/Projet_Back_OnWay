using BackOnWay.Dtos.Utilisateur.Passager;
using BackOnWay.Metier.Utilisateur.Passager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackOnWay.Controllers.Utilisateur.Passager
{
    [Route("[controller]")]
    [ApiController]
    public class DeposerReclamationController : ControllerBase
    {
        private readonly IDeposerReclamationMetier _metier;

        public DeposerReclamationController(IDeposerReclamationMetier metier)
        {
            _metier = metier;
        }

        [HttpPost]
        public IActionResult InsertPlainte([FromBody] DeposerReclamationDto unReclamation)
        {
            int resultat = _metier.InsertPlaintes(unReclamation);

            switch (resultat)
            {
                case 1:
                    // 409 Conflict : Covoiturage non terminé
                    return Conflict(new ProblemDetails
                    {
                        Title = "Covoiturage non terminé",
                        Detail = "Le depôt de plainte est impossible car le covoiturage n'est pas terminé",
                        Status = StatusCodes.Status409Conflict
                    });
                case 2:
                    // 409 Conflict : Plainte déjà existante
                    return Conflict(new ProblemDetails
                    {
                        Title = "Plainte déjà existante",
                        Detail = "Vous avez déjà déposé une plainte pour ce covoiturage.",
                        Status = StatusCodes.Status409Conflict
                    });
                case 3:
                    // 204 Created : succès 
                    return Created();
                case 4:
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
