using System.ComponentModel.DataAnnotations;

namespace BackOnWay.Dtos.Employe
{
    public class AvisUpdateDto
    {
        [Required]
        public int AvisId { get; set; }

        [Required]
        public string AvisStatut { get; set; }
    }
}
