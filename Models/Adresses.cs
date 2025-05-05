using System.ComponentModel.DataAnnotations;

namespace BackOnWay.Models
{
    public class Adresses
    {
        /// <summary>
        /// Identifiant de l'adresse
        /// </summary>
        [Key]
        public int AdresseId { get; set; }

        /// <summary>
        /// Numéros de l'adresse
        /// </summary>
        //[Required(ErrorMessage = "Le numéros est requis.")]
        public byte AdresseNum { get; set; }

        /// <summary>
        /// Nom de la rue
        /// </summary>
        //[Required(ErrorMessage = "Le numéros est requis.")]
        //[StringLength(60, ErrorMessage = "Le nombre de caractère est limité 60.")]
        public string AdresseRue { get; set; }

        /// <summary>
        /// Code postal de l'adresse
        /// </summary>
        // [Required(ErrorMessage = "Le code postal est requis.")]
        // [StringLength(6, ErrorMessage = "Le nombre de caractère est limité 6.")]
        public string AdresseCp { get; set; }

        /// <summary>
        /// Nom de la ville
        /// </summary>
        //[Required(ErrorMessage = "Le nom de la ville est requis.")]
        //[StringLength(60, ErrorMessage = "Le nombre de caractère est limité 60.")]
        public string AdresseVille { get; set; }

        public Adresses() { }
    }
}
