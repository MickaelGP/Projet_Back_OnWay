using System.ComponentModel.DataAnnotations;

namespace BackOnWay.Models
{
    public class Avis
    {
        /// <summary>
        /// Identifiant d" l'avis
        /// </summary>
        [Key]
        public int AvisId { get; set; }

        /// <summary>
        /// Titre de l'avis
        /// </summary>
        [Required(ErrorMessage = "Le titre est requis.")]
        [StringLength(30, ErrorMessage = "Le nombre de caractère est limité 30.")]
        public string AvisTitre { get; set; }

        /// <summary>
        /// Message de l'avis
        /// </summary>
        [Required(ErrorMessage = "Le commentaire est requis.")]
        public string AvisCom { get; set; }

        /// <summary>
        /// Note de l'avis
        /// </summary>
        [Required(ErrorMessage = "La note est requise.")]
        public byte AvisNote { get; set; }

        /// <summary>
        /// Statut de l'avis
        /// </summary>
        [StringLength(12, ErrorMessage = "Le nombre de caractère ne doit pas dépasser 12.")]
        public string AvisStatut { get; set; }

        /// <summary>
        /// Identifiant de l'utilisateur ( auteur )
        /// </summary>
        public int AvisDeposer { get; set; }
        public Utilisateurs UtilisateurDepo { get; set; }
        /// <summary>
        /// Identifiant de l'utilisateur ( expediteur )
        /// </summary>
        public int AvisRecu { get; set; }
        public Utilisateurs UtilisateurRecu { get; set; }
        /// <summary>
        /// Constructeur par defaut
        /// </summary>
        public Avis()
        {
        }
    }
}
