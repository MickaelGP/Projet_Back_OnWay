using System.ComponentModel.DataAnnotations;

namespace BackOnWay.Dtos.Employe
{
    public class AvisUpdateDto
    {
        [Required]
        public int AvisId { get; set; }

        [Required(ErrorMessage = "Le statut est requis.")]
        public string AvisStatut { get; set; }
    }
}
