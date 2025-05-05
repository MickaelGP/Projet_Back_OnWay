using System.ComponentModel.DataAnnotations;

namespace BackOnWay.Models
{
    public class Utilisateurs
    {
        /// <summary>
        /// Identifiant de l'utilisateur
        /// </summary>
        [Key]
        public int UtilId { get; set; }
        /// <summary>
        /// Psuedo de l'utilisateur
        /// </summary>
        [StringLength(30, ErrorMessage = "Le nombre de caractère ne doit pas dépasser 30.")]
        public string UtilPseudo { get; set; }

        /// <summary>
        /// Prénom de l'utilisateur
        /// </summary>
        [Required(ErrorMessage = "Le prénom est requis.")]
        [StringLength(50, ErrorMessage = "Le nombre de caractère ne doit pas dépasser 50.")]
        public string UtilPrenom { get; set; }

        /// <summary>
        /// Nom de l'utilisateur
        /// </summary>
        [Required(ErrorMessage = "Le nom est requis.")]
        [StringLength(50, ErrorMessage = "Le nombre de caractère ne doit pas dépasser 12.")]
        public string UtilNom { get; set; }

        /// <summary>
        /// Date de naissance de l'utilisateur
        /// </summary>
        [Required(ErrorMessage = "La date de naissance est requise.")]
        public DateOnly UtilNaissance { get; set; }

        /// <summary>
        /// Chemin de l'image de profile
        /// </summary>
        //[FileExtensions(Extensions = "jpg,jpeg,png,gif", ErrorMessage = "Les formats accepter sont : jpg, jpeg, png ou gif")]
        public string ImgChemin { get; set; }

        /// <summary>
        /// Nombre de crédit de l'utilisateur
        /// </summary>
        public short UtilCredit { get; set; }

        /// <summary>
        /// Adresse email de l'utilisateur
        /// </summary>
        //[EmailAddress(ErrorMessage = "Adresse email invalide")]
        //[RegularExpression("/^[^\\s@]+@[^\\s@]+\\.[^\\s@]+$/", ErrorMessage = "Format invalide")]
        public string UtilEmail { get; set; }

        /// <summary>
        /// Téléphone de l'utilisateur
        /// </summary>
        //[Required(ErrorMessage = "Le numéros de téléphone est requis.")]
        //[RegularExpression("/^((\\+|00)33\\s?|0)[67](\\s?\\d{2}){4}$/", ErrorMessage = "Format invalide")]
        public string UtilTelephone { get; set; }

        /// <summary>
        /// Mot de passe de l'utilisateur 
        /// </summary>
        //[Required(ErrorMessage = "Le mot de passe est requis.")]
        //[RegularExpression("/^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[\\W_])[A-Za-z\\d\\W_]{8,}$/", ErrorMessage = "Format invalide")]
        public string UtilMdp { get; set; }

        /// <summary>
        /// Statut du compte utilisateur
        /// </summary>
        public bool UtilSuspendu { get; set; }

        /// <summary>
        /// Genre de l'utilisateur
        /// </summary>
        public string UtilGenre { get; set; }
        /// <summary>
        /// Token de session de l'utilisateur
        /// </summary>
        public int UtilToken { get; set; }

        /// <summary>
        /// Rôle de l'utilisateur
        /// </summary>
        public Roles Roles { get; set; }
        public int UtilRole { get; set; }

        //Avis
        public Avis Avis { get; set; }

        //
        public double Note { get; set; }
        public int NombreCom { get; set; }
        /// <summary>
        /// Constructeur par défault
        /// </summary>
        public Utilisateurs()
        {
        }
    }
}
