using System.ComponentModel.DataAnnotations;

namespace BackOnWay.Dtos.Employe
{
    public class PlainteUpdateDto
    {
        [Required]
        public int PlainteId { get; set; }

        [Required(ErrorMessage = "Le statut est requis.")]
        public string PlainteSatut { get; set; }
    }
}
