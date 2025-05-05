using System.ComponentModel.DataAnnotations;

namespace BackOnWay.Models
{
    public class Modeles
    {
        /// <summary>
        /// Identifiant du modéle
        /// </summary>
        [Key]
        public int ModeleId { get; set; }

        /// <summary>
        /// Nom du modéle
        /// </summary>
       // [Required(ErrorMessage = "Le nom du modéle est requis.")]
        //[StringLength(30, ErrorMessage = "Le nombre de caractère est limité 30.")]
        public string ModeleNom { get; set; }

        /// <summary>
        /// Marque du modéle
        /// </summary>
        public Marques Marque { get; set; }
    }
}
