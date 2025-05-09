using BackOnWay.Dtos.Utilisateur.Passager;
using BackOnWay.Metier.Utilisateur.Passager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackOnWay.Controllers.Utilisateur.Passager
{
    [Route("[controller]")]
    [ApiController]
    public class ParticiperCovoitController : ControllerBase
    {
        private readonly IParticiperCovoitMetier _metier;

        public ParticiperCovoitController(IParticiperCovoitMetier metier)
        {
            _metier = metier;
        }

        public IActionResult InsertReservationCovoit(ParticiperCovoitDto unReservation)
        {
            int reponse = _metier.InsertReservationCovoit(unReservation);

            switch (reponse)
            {
                case 1:
                    return Conflict("La réservatin est impossible, le nombre de place restante est  insufisant");
                case 2:
                    return Conflict("Votre solde est insufisant");
                case 3:
                    return NoContent();
                case 4:
                    return StatusCode(500, "Une erreur est survenu lors de l'ajout de la réservation.");
                default:
                    return StatusCode(520, "Une erreur inconnue est survenue.");
            }
        }
    }
}
