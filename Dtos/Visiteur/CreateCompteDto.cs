using System.ComponentModel.DataAnnotations;

namespace BackOnWay.Dtos.Visiteur
{
    public class CreateCompteDto
    {
        [Required]
        public string UtilPseudo { get; set; }

        [Required]
        public string UtilEmail { get; set; }

        [Required]
        public string UtilMdp { get; set; }

        [Required]
        public string UtilGenre { get; set; }
    }
}
