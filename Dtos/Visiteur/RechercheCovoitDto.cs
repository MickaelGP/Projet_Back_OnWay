using System.ComponentModel.DataAnnotations;

namespace BackOnWay.Dtos.Visiteur
{
    public class RechercheCovoitDto
    {
        public int CovoitId { get; set; }

        [Required]
        public DateOnly CovoitDate { get; set; }

        public double CovoitPrix { get; set; }

        public TimeOnly CovoitDepart { get; set; }

        public TimeOnly CovoitArriver { get; set; }

        [Required]
        public string VilleDepart { get; set; }

        [Required]
        public string VilleArriver { get; set; }
    }
}
