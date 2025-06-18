using System.ComponentModel.DataAnnotations;

namespace BackOnWay.Dtos.Utilisateur.Conducteur
{
    public class UpdateCovoitDto
    {
        [Required]
        public int CovoitId { get; set; }

        [Required(ErrorMessage = "Le prix du covoiturage est requis.")]
        public double CovoitPrix { get; set; }

        [Required(ErrorMessage = "La date du covoiturage est requise.")]
        public DateOnly CovoitDate { get; set; }

        [Required(ErrorMessage = "L'heure de départ du covoiturage est requise.")]
        public TimeOnly CovoitDep { get; set; }

        [Required(ErrorMessage = "L'heure d'arrivée du covoiturage est requise.")]
        public TimeOnly CovoitArr { get; set; }
        
        [Required]
        public bool CovoitMusique { get; set; }

        [Required]
        public bool CovoitFumeur { get; set; }
        
        [Required]
        public bool CovoitAnimaux { get; set; }

        public string CovoitStatut { get; set; }
    }
}
