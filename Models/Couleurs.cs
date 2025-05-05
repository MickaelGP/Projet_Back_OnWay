using System.ComponentModel.DataAnnotations;

namespace BackOnWay.Models
{
    public class Couleurs
    {
        /// <summary>
        /// Identifiant de la couleur
        /// </summary>
        [Key]
        public int CouleurId { get; set; }

        /// <summary>
        /// Nom de la couleur
        /// </summary>
       // [Required(ErrorMessage = "Le nom du modéle est requis.")]
        //[StringLength(15, ErrorMessage = "Le nombre de caractère ne doit pas dépasser 15.")]
        public string CouleurNom { get; set; }
    }
}
