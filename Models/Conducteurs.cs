using System.ComponentModel.DataAnnotations;

namespace BackOnWay.Models
{
    public class Conducteurs
    {
        /// <summary>
        /// Identifiant du conducteur
        /// </summary>
        [Key]
        public int ConducteId { get; set; }

        /// <summary>
        /// Date 
        /// </summary>
        public DateOnly ConducteDate { get; set; }

        /// <summary>
        /// Identifiant de l'utilisateur
        /// </summary>
        public Utilisateurs Utilisateur { get; set; }
    }
}
