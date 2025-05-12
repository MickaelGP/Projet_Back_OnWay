using System.ComponentModel.DataAnnotations;

namespace BackOnWay.Dtos.Utilisateur.Passager
{
    public class DeposerReclamationDto
    {
        [Required(ErrorMessage = "La description est requise.")]
        [StringLength(255, ErrorMessage = "Le nombre de caractère ne doit pas dépasser 255.")]
        public string PlainteDescription { get; set; }

        [Required]
        public int UtilId { get; set; }

        [Required]
        public int CovoitId { get; set; }
    }
}
