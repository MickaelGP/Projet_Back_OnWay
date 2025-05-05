using System.ComponentModel.DataAnnotations;

namespace BackOnWay.Dtos.Admin
{
    public class UpdateUtilDto
    {
        [Required]
        public int UtilId { get; set; }

        [Required(ErrorMessage = "Le statut est requis.")]
        public bool UtilSuspendu { get; set; }
    }
}
