using BackOnWay.Dtos.Utilisateur;
using BackOnWay.Metier.Utilisateur;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackOnWay.Controllers.Utilisateur
{
    [Route("[controller]")]
    [ApiController]
    public class ProfilController : ControllerBase
    {
        private ProfilMetier _metier = new ProfilMetier();

        [HttpGet("{unId}")]
        public IActionResult GetProfilUtil(int unId)
        {
            UtilProfilDto? unProfil = _metier.GetProfilUtil(unId);

            if (unProfil == null)
            {
                return NotFound("Profil non trouvé !");
            }

            return Ok(unProfil);
        }

        [HttpPut("update-profil")]
        public IActionResult UpdateProfil([FromBody] UtilProfilDto utilProfil)
        {

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
        public IActionResult DeleteProfilUtil([FromBody] int unId)
        {
            bool resultat = _metier.DeleteProfilUtil(unId);

            if (!resultat)
            {
                return StatusCode(500, "Une erreur s'est produite lors de la mise à jour du profil !");
            }

            return NoContent();
        }
    }
}
