using BackOnWay.Dtos.Admin;
using BackOnWay.Dtos.Auth;
using BackOnWay.Metier.Admin;
using BackOnWay.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackOnWay.Controllers.Admin
{
    [Route("[controller]")]
    [ApiController]
    public class ListeCompteController : ControllerBase
    {
        private ListeCompteMetier _metier = new ListeCompteMetier();

        [HttpGet]
        public IActionResult GetListeCompte()
        {
            ConnexionInfoDto? util = HttpContext.Items["Utilisateur"] as ConnexionInfoDto;
            if (util == null || util.RoleLabel != "Admin")
            {
                return Unauthorized("Accés refusé");
            }
            List<UtilisateurDto> utilisateurs = _metier.GetListeCompte();
            return Ok(utilisateurs);
        }
        [HttpGet("{unId}")]
        public IActionResult SelectCompte(int unId)
        {
            UtilisateurDto unUtil = _metier.InfoCompte(unId);

            if (unUtil == null)
            {
                return NotFound("Utilisateur non trouvé");
            }

            return Ok(unUtil);
        }
        [HttpPost("ajout")]
        public IActionResult AjoutEmploye([FromBody] AddEmployeDto unEmploye)
        {
            if (unEmploye == null)
            {
                return new JsonResult("L'élément n'est pas valide");
            }

            string mdpHash = BCrypt.Net.BCrypt.HashPassword(unEmploye.UtilMdp, 12);
            unEmploye.UtilMdp = mdpHash;

            int ajouter = _metier.AjoutEmploye(unEmploye);

            switch (ajouter)
            {
                case 0:
                    return Conflict("L'employé existe déjà.");
                case 1:
                    new SendMailUtils().SendEmailNewEmploye(unEmploye);
                    return Created("", "Ajout effectué avec succès !");
                case 2:
                    return StatusCode(500, "Une erreur s'est produite lors de l'ajout de l'employé.");
                default:
                    return StatusCode(520, "Une erreur inconnue est survenue.");
            }
        }
        [HttpDelete("delete")]
        public IActionResult SupEmployeCpte([FromBody] int unId)
        {
            if (unId == 0)
            {
                return BadRequest("L'élément n'est pas valide");
            }
            bool supprimer = _metier.SupEmployeCpte(unId);
            string reponse;

            if (!supprimer)
            {
                return StatusCode(500, "Une erreur s'est produite lors de la suppression du compte de l'employé.");
            }

            return NoContent();

        }

        [HttpPut("update")]
        public IActionResult MajStatut([FromBody] UpdateUtilDto unUtil)
        {
            if (unUtil == null)
            {
                return BadRequest("L'élément n'est pas valide");
            }
            bool modif = _metier.MajStatut(unUtil);
            string reponse;
            if (!modif)
            {
                return StatusCode(500, "Une erreur s'est produite lors de la mise à jour du statut de l'utilisateur.");
            }

            return NoContent();

        }
    }
}
