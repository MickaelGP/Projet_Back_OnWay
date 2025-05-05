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
        public string UtilPseudo { get; set; }

        /// <summary>
        /// Prénom de l'utilisateur
        /// </summary>
        public string UtilPrenom { get; set; }

        /// <summary>
        /// Nom de l'utilisateur
        /// </summary>
        public string UtilNom { get; set; }

        /// <summary>
        /// Date de naissance de l'utilisateur
        /// </summary>
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
        public string UtilEmail { get; set; }

        /// <summary>
        /// Téléphone de l'utilisateur
        /// </summary>
        public string UtilTelephone { get; set; }

        /// <summary>
        /// Mot de passe de l'utilisateur 
        /// </summary>
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

        /// <summary>
        /// Note recu
        /// </summary>
        public double Note { get; set; }

        /// <summary>
        /// Nombre de commentaire
        /// </summary>
        public int NombreCom { get; set; }

        /// <summary>
        /// Constructeur par défault
        /// </summary>
        public Utilisateurs()
        {
        }
    }
}
