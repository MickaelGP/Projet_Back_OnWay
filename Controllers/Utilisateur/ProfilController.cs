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
                return Unauthorized("Utilisateur non connecté.");
            }

            UtilProfilDto? unProfil = _metier.GetProfilUtil(util.UtilId);

            if (unProfil == null)
            {
                return NotFound("Profil non trouvé !");
            }

            return Ok(unProfil);
        }

        [HttpPut("update-profil")]
        public IActionResult UpdateProfil([FromBody] UtilProfilDto utilProfil)
        {
            ConnexionInfoDto? util = HttpContext.Items["Utilisateur"] as ConnexionInfoDto;
            if (util == null)
            {
                return Unauthorized("Utilisateur non connecté.");
            }
            utilProfil.UtilId = util.UtilId;
            int infoProfil = _metier.UpdateProfil(utilProfil);

            switch (infoProfil)
            {
                case 0:
                    return Conflict("L'email existe déjà.");
                case 1:
                    return StatusCode(500, "Une erreur s'est produite lors de la mise à jour du profil !");
                case 2:
                    return NoContent();
                default:
                    return StatusCode(520, "Une erreur inconnue est survenue.");
            }
        }

        [HttpPut("update-mot-de-passe")]
        public IActionResult UpdateMdpUtil(UpdateMdpUtilDto utilMdpUtil)
        {
            ConnexionInfoDto? util = HttpContext.Items["Utilisateur"] as ConnexionInfoDto;
            if (util == null)
            {
                return Unauthorized("Utilisateur non connecté.");
            }
            utilMdpUtil.UtilId = util.UtilId;
            int resultat = _metier.UpdateMdpUtil(utilMdpUtil);

            switch (resultat)
            {
                case 0:
                    return BadRequest("L'ancien mot de passe ne correspond pas avec nos informations !");
                case 1:
                    return StatusCode(500, "Une erreur s'est produite lors de la mise à jour du mot de passe");
                case 2:
                    return NoContent();
                default:
                    return StatusCode(520, "Une erreur inconnue est survenue.");
            }
        }

        [HttpDelete("/delete-profil")]
        public IActionResult DeleteProfilUtil()
        {
            ConnexionInfoDto? util = HttpContext.Items["Utilisateur"] as ConnexionInfoDto;
            if (util == null)
            {
                return Unauthorized("Utilisateur non connecté.");
            }
            bool resultat = _metier.DeleteProfilUtil(util.UtilId);

            if (!resultat)
            {
                return StatusCode(500, "Une erreur s'est produite lors de la suppression du profil !");
            }

            return NoContent();
        }
    }
}
