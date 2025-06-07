using BackOnWay.Dtos.Auth;
using BackOnWay.Dtos.Utilisateur.Conducteur;
using BackOnWay.Metier.Utilisateur.Conducteur;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackOnWay.Controllers.Utilisateur.Conducteur
{
    [Route("[controller]")]
    [ApiController]
    public class AjoutCovoiturageController : ControllerBase
    {
        private AjoutCovoiturageMetier _metier = new AjoutCovoiturageMetier();


        [HttpGet("/liste-voitures")]
        public IActionResult GetUtilVoitures()
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
                List<GetListeVoitureDto> listeVoitures = _metier.GetUtilVoitures(util.UtilId);

            return Ok(listeVoitures);
        }

        [HttpPost("/ajouter-covoiturage")]
        public IActionResult InsertCovoiturage([FromBody] InsertCovoiturageDto unCovoiturage)
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
            unCovoiturage.UtilId = util.UtilId;
            int reponse = _metier.InsertCovoiturage(unCovoiturage);

            switch (reponse)
            {
                case 1:
                    return StatusCode(409, new ProblemDetails
                    {
                        Title = "Conflit de date",
                        Detail = "Vous avez déjà un covoiturage de prévu à cette date.",
                        Status = StatusCodes.Status409Conflict
                    });
                case 2:
                    return StatusCode(409, new ProblemDetails
                    {
                        Title = "Solde insuffisant",
                        Detail = "Vous ne pouvez pas faire de réservation, car votre solde de crédit est insuffisant.",
                        Status = StatusCodes.Status409Conflict
                    });
                case 3:
                    return Ok(new { message = "Covoiturage ajouté" });
                case 4:
                    return StatusCode(500, new ProblemDetails
                    {
                        Title = "Erreur interne",
                        Detail = "Une erreur s'est produite lors de la création du covoiturage !",
                        Status = StatusCodes.Status500InternalServerError
                    });
                default:
                    return StatusCode(500, new ProblemDetails
                    {
                        Title = "Erreur inconnue",
                        Detail = "Une erreur inconnue est survenue.",
                        Status = StatusCodes.Status500InternalServerError
                    });
            }
        }
    }
}
