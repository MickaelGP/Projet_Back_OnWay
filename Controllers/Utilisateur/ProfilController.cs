using BackOnWay.Dtos.Auth;
using BackOnWay.Dtos.Utilisateur;
using BackOnWay.Metier.Utilisateur;
using BackOnWay.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackOnWay.Controllers.Utilisateur
{
    [Route("[controller]")]
    [ApiController]
    public class ProfilController : ControllerBase
    {
        private ProfilMetier _metier = new ProfilMetier();

        [HttpGet("/info-profil")]
        public IActionResult GetProfilUtil()
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

            UtilProfilDto? unProfil = _metier.GetProfilUtil(util.UtilId);

            if (unProfil == null)
            {
                return StatusCode(404, new ProblemDetails
                {
                    Title = "Aucun résultat",
                    Detail = "Profil non trouvé !",
                    Status = StatusCodes.Status500InternalServerError
                });
            }

            return Ok(unProfil);
        }

        [HttpPut("/update-profil")]
        public IActionResult UpdateProfil([FromBody] UtilProfilDto utilProfil)
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
            utilProfil.UtilId = util.UtilId;
            int infoProfil = _metier.UpdateProfil(utilProfil);

            switch (infoProfil)
            {
                case 0:
                    return StatusCode(409, new ProblemDetails
                    {
                        Title = "Email existant",
                        Detail = "L'email existe déjà.",
                        Status = StatusCodes.Status409Conflict
                    });
                case 1:
                    return StatusCode(500, new ProblemDetails
                    {
                        Title = "Erreur de mise à jour",
                        Detail = "Une erreur s'est produite lors de la mise à jour du profil !",
                        Status = StatusCodes.Status500InternalServerError
                    });
                case 2:
                    return Ok(new { message = "Profil mis à jour" });
                default:
                    return StatusCode(500, new ProblemDetails
                    {
                        Title = "Erreur inconnue",
                        Detail = "Une erreur inconnue est survenue.",
                        Status = StatusCodes.Status500InternalServerError
                    });
            }
        }

        [HttpPut("/update-mot-de-passe")]
        public IActionResult UpdateMdpUtil(UpdateMdpUtilDto utilMdpUtil)
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
            utilMdpUtil.UtilId = util.UtilId;
            int resultat = _metier.UpdateMdpUtil(utilMdpUtil);

            switch (resultat)
            {
                case 0:
                    return StatusCode(400, new ProblemDetails
                    {
                        Title = "Aucune coresspondance",
                        Detail = "L'ancien mot de passe ne correspond pas avec nos informations !",
                        Status = StatusCodes.Status400BadRequest
                    });
                case 1:
                    return StatusCode(500, new ProblemDetails
                    {
                        Title = "Erreur de mise à jour",
                        Detail = "Une erreur s'est produite lors de la mise à jour du mot de passe",
                        Status = StatusCodes.Status500InternalServerError
                    });
                case 2:
                    return Ok(new { message = "Mot de passe mis à jour" });
                default:
                    return StatusCode(500, new ProblemDetails
                    {
                        Title = "Erreur inconnue",
                        Detail = "Une erreur inconnue est survenue.",
                        Status = StatusCodes.Status500InternalServerError
                    });
            }
        }

        [HttpDelete("/delete-profil")]
        public IActionResult DeleteProfilUtil()
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
            bool resultat = _metier.DeleteProfilUtil(util.UtilId);

            if (!resultat)
            {
                return StatusCode(500, new ProblemDetails
                {
                    Title = "Erreur de suppression",
                    Detail = "Une erreur s'est produite lors de la suppression du profil !",
                    Status = StatusCodes.Status500InternalServerError
                });
            }

            return Ok(new { message = "Compte supprimé avec succès" });
        }
    }
}
