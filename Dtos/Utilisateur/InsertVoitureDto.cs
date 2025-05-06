using System.ComponentModel.DataAnnotations;

namespace BackOnWay.Dtos.Utilisateur
{
    public class InsertVoitureDto
    {
        [Required]
        public DateOnly VoitDateImat { get; set; }

        [Required]
        [StringLength(10, ErrorMessage ="Le nombre de caractére est limité à 10.")]
        public string VoitEnergie { get; set; }

        [Required]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Il faut 9 caractéres au minimun et au maximun")]
        public string VoitPlaque { get; set; }

        [Required]
        public int VoitNbSiege { get; set; }

        [Required]
        public int VoitModele { get; set; }

        [Required]
        public int VoitCouleur { get; set; }

        [Required]
        public int UtilId { get; set; }
    }
}
