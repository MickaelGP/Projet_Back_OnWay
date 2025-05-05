using System.ComponentModel.DataAnnotations;

namespace BackOnWay.Dtos.Auth
{
    public class ConnexionDto
    {
        [Required]
        public string UtilEmail { get; set; }

        [Required]
        public string UtilMdp { get; set; }
    }
}
