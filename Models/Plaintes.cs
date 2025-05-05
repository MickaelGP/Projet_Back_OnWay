using System.ComponentModel.DataAnnotations;

namespace BackOnWay.Models
{
    public class Plaintes
    {
        /// <summary>
        /// Identifiant de la plainte
        /// </summary>
        [Key]
        public int PlainteId { get; set; }

        /// <summary>
        /// Déscription de la plainte
        /// </summary>
        [Required(ErrorMessage = "La description est requise.")]
        [StringLength(255, ErrorMessage = "Le nombre de caractère ne doit pas dépasser 255.")]
        public string PlainteDescription { get; set; }

        /// <summary>
        /// Statut de la plainte
        /// </summary>
        [StringLength(20, ErrorMessage = "Le nombre de caractère ne doit pas dépasser 20.")]
        public string PlainteSatut { get; set; }

        /// <summary>
        /// Identifiant de la réservation
        /// </summary>
        public int PlainteResa { get; set; }
        public Reservations Reservations { get; set; }

        public Plaintes()
        {
        }
    }
}
