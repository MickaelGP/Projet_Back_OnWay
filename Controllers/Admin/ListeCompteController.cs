using BackOnWay.Dtos.Admin; // Importation des DTOs utilisés pour le transfert de données de l'utilisateur
using BackOnWay.Metier.Admin; // Importation de la logique métier qui gère les comptes utilisateurs et employés
using BackOnWay.Utils; // Importation des utilitaires, notamment pour l'envoi d'e-mails
using Microsoft.AspNetCore.Mvc; // Importation pour définir des contrôleurs et actions API dans ASP.NET Core

namespace BackOnWay.Controllers.Admin
{
    // Définition du contrôleur API pour la gestion des comptes utilisateurs et employés
    [Route("[controller]")] // La route de base pour ce contrôleur, il répondra à /ListeCompte
    [ApiController] // Cette annotation indique qu'il s'agit d'un contrôleur API
    public class ListeCompteController : ControllerBase
    {
        // Déclaration d'une instance de la classe ListeCompteMetier qui contient la logique métier
        private readonly ListeCompteMetier _metier;

        // Constructeur du contrôleur qui reçoit une instance de ListeCompteMetier via l'injection de dépendances
        public ListeCompteController(ListeCompteMetier metier)
        {
            _metier = metier; // Initialisation de l'instance _metier avec l'instance reçue en paramètre
        }

        /// <summary>
        /// Action pour récupérer la liste de tous les comptes utilisateurs
        /// </summary>
        /// <returns>Retourne une liste d'Utilisateurs en JSON</returns>
        [HttpGet] // Indique que cette action répond à une requête GET
        public IActionResult GetListeCompte()
        {
            // Appel à la méthode GetListeCompte du métier pour récupérer la liste des utilisateurs
            List<UtilisateurDto> utilisateurs = _metier.GetListeCompte();
            return Ok(utilisateurs); // Retourne les utilisateurs sous forme d'une réponse HTTP 200 OK avec les données
        }

        /// <summary>
        /// Action pour récupérer un compte utilisateur spécifique
        /// </summary>
        /// <param name="unId">ID de l'utilisateur à rechercher</param>
        /// <returns>Retourne les informations de l'utilisateur ou un message d'erreur</returns>
        [HttpGet("{unId}")] // La route inclut un paramètre d'ID, ce qui signifie /ListeCompte/{unId}
        public IActionResult SelectCompte(int unId)
        {
            // Appel à la méthode InfoCompte du métier pour récupérer les infos de l'utilisateur
            UtilisateurDto unUtil = _metier.InfoCompte(unId);

            // Si l'utilisateur n'est pas trouvé, retourne une erreur 404 Not Found
            if (unUtil == null)
            {
                return NotFound("Utilisateur non trouvé");
            }

            // Si l'utilisateur est trouvé, retourne ses informations avec une réponse HTTP 200 OK
            return Ok(unUtil);
        }

        /// <summary>
        /// Action pour ajouter un nouvel employé
        /// </summary>
        /// <param name="unEmploye">DTO contenant les informations du nouvel employé</param>
        /// <returns>Retourne le statut de l'opération (succès, conflit, erreur serveur)</returns>
        [HttpPost("ajout")] // Cette action répond à une requête POST à /ListeCompte/ajout
        public IActionResult AjoutEmploye([FromBody] AddEmployeDto unEmploye)
        {
            // Vérification si le DTO reçu est valide
            if (unEmploye == null)
            {
                return new JsonResult("L'élément n'est pas valide");
            }

            // Hashage du mot de passe de l'employé avant de l'ajouter à la base de données
            string mdpHash = BCrypt.Net.BCrypt.HashPassword(unEmploye.UtilMdp, 12);
            unEmploye.UtilMdp = mdpHash;

            // Appel à la méthode AjoutEmploye du métier pour ajouter l'employé
            int ajouter = _metier.AjoutEmploye(unEmploye);

            // Gestion du code retour et des réponses selon le résultat de l'ajout
            switch (ajouter)
            {
                case 0:
                    return Conflict("L'employé existe déjà."); // Retourne 409 Conflict si l'employé existe déjà
                case 1:
                    // Si l'ajout a réussi, on envoie un e-mail de notification à l'employé et retourne un message de succès
                    new SendMailUtils().SendEmailNewEmploye(unEmploye);
                    return Created("", "Ajout effectué avec succès !"); // Retourne 201 Created avec un message de succès
                case 2:
                    // Si une erreur interne s'est produite lors de l'ajout, retourne une erreur 500
                    return StatusCode(500, "Une erreur s'est produite lors de l'ajout de l'employé.");
                default:
                    // Si une erreur inconnue s'est produite, retourne une erreur générique
                    return StatusCode(520, "Une erreur inconnue est survenue.");
            }
        }

        /// <summary>
        /// Action pour supprimer un employé par son ID
        /// </summary>
        /// <param name="unId">ID de l'employé à supprimer</param>
        /// <returns>Retourne un statut de l'opération (succès ou erreur)</returns>
        [HttpDelete("delete")] // Cette action répond à une requête DELETE à /ListeCompte/delete
        public IActionResult SupEmployeCpte([FromBody] int unId)
        {
            // Si l'ID est invalide (0), retourne une erreur 400 BadRequest
            if (unId == 0)
            {
                return BadRequest("L'élément n'est pas valide");
            }

            // Appel à la méthode SupEmployeCpte du métier pour supprimer l'employé
            bool supprimer = _metier.SupEmployeCpte(unId);

            // Si la suppression échoue, retourne une erreur 500
            if (!supprimer)
            {
                return StatusCode(500, "Une erreur s'est produite lors de la suppression du compte de l'employé.");
            }

            // Si la suppression réussit, retourne une réponse 204 No Content
            return NoContent();
        }

        /// <summary>
        /// Action pour mettre à jour le statut (suspendre ou réactiver) d'un utilisateur
        /// </summary>
        /// <param name="unUtil">DTO contenant les informations de l'utilisateur à modifier</param>
        /// <returns>Retourne un statut de l'opération (succès ou erreur)</returns>
        [HttpPut("update")] // Cette action répond à une requête PUT à /ListeCompte/update
        public IActionResult MajStatut([FromBody] UpdateUtilDto unUtil)
        {
            // Si le DTO reçu est invalide, retourne une erreur 400 BadRequest
            if (unUtil == null)
            {
                return BadRequest("L'élément n'est pas valide");
            }

            // Appel à la méthode MajStatut du métier pour mettre à jour le statut de l'utilisateur
            bool modif = _metier.MajStatut(unUtil);

            // Si la mise à jour échoue, retourne une erreur 500
            if (!modif)
            {
                return StatusCode(500, "Une erreur s'est produite lors de la mise à jour du statut de l'utilisateur.");
            }

            // Si la mise à jour réussit, retourne une réponse 204 No Content
            return NoContent();
        }
    }
}
