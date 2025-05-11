using BackOnWay.Dtos.Utilisateur.Passager;
using BackOnWay.Metier.Utilisateur.Passager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackOnWay.Controllers.Utilisateur.Passager
{
    [Route("[controller]")]
    [ApiController]
    public class DeposerAvisController : ControllerBase
    {
        private readonly IDeposerAvisMetier _metier;

        public DeposerAvisController(IDeposerAvisMetier metier)
        {
            _metier = metier;
        }

        [HttpPost]
        public IActionResult InsertAvis([FromBody] DeposerAvisDto unAvis)
        {
            int response = _metier.InsertAvis(unAvis);

            switch (response)
            {
                case 1:
                    // 409 Conflict : Covoiturage non Terminé
                    return Conflict(new ProblemDetails
                    {
                        Title = "Covoiturage non  terminé",
                        Detail = "L'ajout de l'avis est impossible car le covoiturages n'est pas fini.",
                        Status = StatusCodes.Status409Conflict
                    });
                case 2:
                    // 500 Internal Server Error : erreur technique
                    return StatusCode(500, new ProblemDetails
                    {
                        Title = "Erreur interne",
                        Detail = "Une erreur est survenue lors de l'ajout de l'avis.",
                        Status = StatusCodes.Status500InternalServerError
                    });
                case 3:
                    // 204 Created : succès 
                    return Created();
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
