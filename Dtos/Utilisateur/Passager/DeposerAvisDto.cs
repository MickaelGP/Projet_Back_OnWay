using BackOnWay.Models;
using System.ComponentModel.DataAnnotations;

namespace BackOnWay.Dtos.Utilisateur.Passager
{
    public class DeposerAvisDto
    {
        [Required]
        public int CovoitId { get; set; }

        [Required(ErrorMessage = "Le titre est requis.")]
        [StringLength(30, ErrorMessage = "Le nombre de caractère est limité 30.")]
        public string AvisTitre { get; set; }

        [Required(ErrorMessage = "Le commentaire est requis.")]
        public string AvisCom { get; set; }

        public byte AvisNote { get; set; }

        public int AvisDeposer { get; set; }
    }
}
