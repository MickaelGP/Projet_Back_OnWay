using System.ComponentModel.DataAnnotations;

namespace BackOnWay.Models
{
    public class Marques
    {
        /// <summary>
        /// Identifiant de la marque
        /// </summary>
        [Key]
        public int MarqueId { get; set; }

        /// <summary>
        /// Nom de la marque
        /// </summary>
        // [Required(ErrorMessage = "Le nom de la marque est requis.")]
        // [StringLength(30, ErrorMessage = "Le nombre de caractère est limité 30.")]
        public string MarqueNom { get; set; }
    }
}
