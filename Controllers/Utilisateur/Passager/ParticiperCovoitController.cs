using BackOnWay.Dtos.Utilisateur.Passager;
using BackOnWay.Metier.Utilisateur.Passager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackOnWay.Controllers.Utilisateur.Passager
{
    // Déclare le contrôleur d'API avec une route basée sur le nom du contrôleur ("ParticiperCovoit")
    [Route("[controller]")]
    [ApiController]
    public class ParticiperCovoitController : ControllerBase
    {
        // Injection de dépendance de la couche métier (logique métier pour la réservation)
        private readonly IParticiperCovoitMetier _metier;

        public ParticiperCovoitController(IParticiperCovoitMetier metier)
        {
            _metier = metier;
        }

        // Endpoint HTTP POST pour insérer une réservation de covoiturage
        [HttpPost]
        public IActionResult InsertReservationCovoit(ParticiperCovoitDto unReservation)
        {
            // Appel de la méthode métier pour insérer la réservation
            int reponse = _metier.InsertReservationCovoit(unReservation);

            // Traitement du code de retour métier et mapping vers un code HTTP approprié
            switch (reponse)
            {
                case 1:
                    // 409 Conflict : pas assez de places
                    return Conflict(new ProblemDetails
                    {
                        Title = "Conflit de réservation",
                        Detail = "La réservation est impossible, le nombre de places restantes est insuffisant.",
                        Status = StatusCodes.Status409Conflict
                    });
                case 2:
                    // 409 Conflict : solde insuffisant
                    return Conflict(new ProblemDetails
                    {
                        Title = "Solde insuffisant",
                        Detail = "Votre solde est insuffisant pour effectuer cette réservation.",
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
