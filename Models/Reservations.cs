using System.ComponentModel.DataAnnotations;

namespace BackOnWay.Models
{
    public class Reservations
    {
        [Key]
        public int ResaId { get; set; }

        /// <summary>
        /// Statut de la reservation
        /// </summary>
        public string ResaStatut { get; set; }

        /// <summary>
        /// Nombre de siege réservé
        /// </summary>
        //[Required(ErrorMessage = "Le nombre de siége est requis.")]
        public byte ResaNbSiege { get; set; }

        /// <summary>
        /// Date de la réservation
        /// </summary>
        //[Required(ErrorMessage = "La date est requise.")]
        public DateOnly ResaDate { get; set; }

        /// <summary>
        ///Identifiant de l'utilisateur
        /// </summary>
        public int ResaUtil { get; set; }
        public Utilisateurs Utilisateurs { get; set; }

        /// <summary>
        /// Identifinat du covoiturage
        /// </summary>
        public int ResaCovoit { get; set; }
        public Covoiturages Covoiturages { get; set; }

        /// <summary>
        /// Constructeur par default 
        /// </summary>
        public Reservations() { }
    }
}
