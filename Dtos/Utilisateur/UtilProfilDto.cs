using System.ComponentModel.DataAnnotations;

namespace BackOnWay.Dtos.Utilisateur
{
    public class UtilProfilDto
    {
        [Required]
        public int UtilId { get; set; }

        [Required]
        public string UtilPseudo { get; set; }

        [Required]
        public string UtilPrenom { get; set; }

        [Required]
        public string UtilNom { get; set; }

        [Required]
        public DateOnly UtilNaissance { get; set; }

        public short UtilCredit { get; set; }

        [Required]
        public string UtilEmail { get; set; }

        [Required]
        public string UtilTelephone { get; set; }

        [Required]
        public string UtilGenre { get; set; }
    }
}
