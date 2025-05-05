using System.ComponentModel.DataAnnotations;

namespace BackOnWay.Dtos.Employe
{
    public class AvisDto
    {
        public int AvisId { get; set; }

        public string AvisTitre { get; set; }

        [Required(ErrorMessage = "Le statut est requis.")]
        public string AvisStatut { get; set; }
    }
}
