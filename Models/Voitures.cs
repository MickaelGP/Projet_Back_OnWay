using System.ComponentModel.DataAnnotations;

namespace BackOnWay.Models
{
    public class Voitures
    {
        /// <summary>
        /// Identifiant de la voiture
        /// </summary>
        [Key]
        public int VoitId { get; set; }

        /// <summary>
        /// Date de l'imatriculation de la voiture 
        /// </summary>
        public DateOnly VoitDateImat { get; set; }

        /// <summary>
        /// Couleur de la vouture
        /// </summary>
        //[Key]
        //[Required(ErrorMessage = "La couleur est requise.")]
        public int VoitCouleur { get; set; }
        public Couleurs Couleurs { get; set; }
        /// <summary>
        /// Type de voiture ( Essence )
        /// </summary>
        //[Required(ErrorMessage = "L'énergie du véhicule est requise.")]
        //[StringLength(10, ErrorMessage = "Le nombre de caractère est limité 10.")]
        public string VoitEnergie { get; set; }

        /// <summary>
        /// Plaque d'immatriculation du véhicule
        /// </summary>
        //[Required(ErrorMessage = "La plaque d'imatriculation est requise.")]
        // [StringLength(9, ErrorMessage = "Le nombre de caractère est limité 9.")]
        public string VoitPlaque { get; set; }

        /// <summary>
        /// Identifiant du modéle de véhicule
        /// </summary>
        //[Key]
        public int VoitModele { get; set; }
        public Modeles Modeles { get; set; }
        /// <summary>
        /// Identifiant du conducteur
        /// </summary>
       // [Key]
        public int VoitConduc { get; set; }
        public Conducteurs Conducteurs { get; set; }
        /// <summary>
        /// Nombre de siege du véhicule
        /// </summary>
       // [Required(ErrorMessage = "Le nombre de siége est requis.")]
        public byte VoitNbSiege { get; set; }
    }
}
