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
        // Appel à la couche métier pour le traitement des employés
        private ListeCompteMetier _metier = new ListeCompteMetier();

        // Endpoint pour l’ajout d’un nouvel employé via une requête POST
        [HttpPost("/ajout-compte-employé")]
        public IActionResult AjoutEmploye([FromBody] AddEmployeDto unEmploye)
        {
            // Récupère les infos de l’utilisateur connecté depuis le contexte HTTP
            ConnexionInfoDto? util = HttpContext.Items["Utilisateur"] as ConnexionInfoDto;

            // Vérifie si l’utilisateur est connecté et possède le rôle "Admin"
            if (util == null || util.RoleLabel != "Admin")
            {
                return StatusCode(401, new ProblemDetails
                {
                    Title = "Token manquant",
                    Detail = "Utilisateur non connecté.",
                    Status = StatusCodes.Status401Unauthorized
                });
            }
            // Hachage du mot de passe de l’employé pour garantir sa sécurité
            string mdpHash = BCrypt.Net.BCrypt.HashPassword(unEmploye.UtilMdp, 12);
            unEmploye.UtilMdp = mdpHash;

            // Appel de la méthode métier pour ajouter l’employé
            int ajouter = _metier.AjoutEmploye(unEmploye);

            // Traitement du code de retour pour adapter la réponse HTTP
            switch (ajouter)
            {
                // Cas où l’employé existe déjà
                case 0:
                    return StatusCode(409, new ProblemDetails
                    {
                        Title = "Compte déjà existant",
                        Detail = "L'employé existe déjà",
                        Status = StatusCodes.Status409Conflict
                    });
                // Cas où l’ajout est réussi : on envoie un mail de bienvenue
                case 1:
                    new SendMailUtils().SendEmailNewEmploye(unEmploye);
                    return Ok(new { message = "Employé ajouté" });
                // Erreur lors de l'ajout en base
                case 2:
                    return StatusCode(500, new ProblemDetails
                    {
                        Title = "Erreur lors de l'ajout",
                        Detail = "Une erreur s'est produite lors de l'ajout de l'employé.",
                        Status = StatusCodes.Status500InternalServerError
                    });
                default:
                    // Cas inattendu
                    return StatusCode(500, new ProblemDetails
                    {
                        Title = "Erreur inconnue",
                        Detail = "Une erreur inconnue est survenue.",
                        Status = StatusCodes.Status500InternalServerError
                    });
            }
        }
        [HttpGet("/liste-comptes")]
        public IActionResult GetListeCompte()
        {
            ConnexionInfoDto? util = HttpContext.Items["Utilisateur"] as ConnexionInfoDto;
            if (util == null || util.RoleLabel != "Admin")
            {
                return StatusCode(401, new ProblemDetails
                {
                    Title = "Token manquant",
                    Detail = "Utilisateur non connecté.",
                    Status = StatusCodes.Status401Unauthorized
                });
            }
            List<UtilisateurDto> utilisateurs = _metier.GetListeCompte();
            return Ok(utilisateurs);
        }
        [HttpGet("/details-compte/{unId}")]
        public IActionResult SelectCompte(int unId)
        {
            ConnexionInfoDto? util = HttpContext.Items["Utilisateur"] as ConnexionInfoDto;
            if (util == null || util.RoleLabel != "Admin")
            {
                return StatusCode(401, new ProblemDetails
                {
                    Title = "Token manquant",
                    Detail = "Utilisateur non connecté.",
                    Status = StatusCodes.Status401Unauthorized
                });
            }
            UtilisateurDto unUtil = _metier.InfoCompte(unId);

            if (unUtil == null)
            {
                return StatusCode(404, new ProblemDetails
                {
                    Title = "Utilisateur inconnue",
                    Detail = "Aucun Utilisateur trouvé.",
                    Status = StatusCodes.Status404NotFound
                });
            }

            return Ok(unUtil);
        }
        [HttpDelete("/delete-compte-employé")]
        public IActionResult SupEmployeCpte([FromBody] int unId)
        {
            ConnexionInfoDto? util = HttpContext.Items["Utilisateur"] as ConnexionInfoDto;
            if (util == null || util.RoleLabel != "Admin")
            {
                return StatusCode(401, new ProblemDetails
                {
                    Title = "Token manquant",
                    Detail = "Utilisateur non connecté.",
                    Status = StatusCodes.Status401Unauthorized
                });
            }
            bool supprimer = _metier.SupEmployeCpte(unId);

            if (!supprimer)
            {
                return StatusCode(500, new ProblemDetails
                {
                    Title = "Une erreur est survenue",
                    Detail = "Une erreur s'est produite lors de la suppression du compte de l'employé.",
                    Status = StatusCodes.Status500InternalServerError
                });
            }

            return Ok(new { message = "Utilisateur supprimé" });

        }

        [HttpPut("/update-compte")]
        public IActionResult MajStatut([FromBody] UpdateUtilDto unUtil)
        {
            ConnexionInfoDto? util = HttpContext.Items["Utilisateur"] as ConnexionInfoDto;
            if (util == null || util.RoleLabel != "Admin")
            {
                return StatusCode(401, new ProblemDetails
                {
                    Title = "Token manquant",
                    Detail = "Utilisateur non connecté.",
                    Status = StatusCodes.Status401Unauthorized
                });
            }
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

            return Ok(new { message = "Statut mis à jour" });

        }
    }
}
