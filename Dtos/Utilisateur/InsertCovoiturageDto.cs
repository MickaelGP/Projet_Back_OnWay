using System.ComponentModel.DataAnnotations;

namespace BackOnWay.Dtos.Utilisateur
{
    public class InsertCovoiturageDto
    {
        [Required(ErrorMessage = "L'identifiant utilisateur est obligatoire.")]
        public int UtilId { get; set; }

        [Required(ErrorMessage = "L'identifiant de la voiture est obligatoire.")]
        public int VoitId { get; set; }

        [Required(ErrorMessage = "La date du covoiturage est obligatoire.")]
        public DateOnly CovoitDate { get; set; }

        [Required(ErrorMessage = "Le numéro de l'adresse de départ est obligatoire.")]
        public byte AdresseNumDepart { get; set; }

        [Required(ErrorMessage = "Le nom de rue de départ est obligatoire.")]
        [StringLength(60, ErrorMessage = "Le nom de rue ne peut pas dépasser 60 caractères.")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ0-9\s\-']+$", ErrorMessage = "Le nom de rue de départ contient des caractères invalides.")]
        public string AdresseRueDepart { get; set; }

        [Required(ErrorMessage = "Le code postal de départ est obligatoire.")]
        [StringLength(6, ErrorMessage = "Le code postal ne peut pas dépasser 6 caractères.")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Le code postal de départ doit être composé de 5 chiffres.")]
        public string AdresseCpDepart { get; set; }

        [Required(ErrorMessage = "Le nom de la ville de départ est obligatoire.")]
        [StringLength(60, ErrorMessage = "Le nom de la ville ne peut pas dépasser 60 caractères.")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\s\-']+$", ErrorMessage = "Le nom de la ville de départ contient des caractères invalides.")]
        public string AdresseVilleDepart { get; set; }

        [Required(ErrorMessage = "Le numéro de l'adresse d'arrivée est obligatoire.")]
        public byte AdresseNumArriver { get; set; }

        [Required(ErrorMessage = "Le nom de rue d'arrivée est obligatoire.")]
        [StringLength(60, ErrorMessage = "Le nom de rue ne peut pas dépasser 60 caractères.")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ0-9\s\-']+$", ErrorMessage = "Le nom de rue d'arrivée contient des caractères invalides.")]
        public string AdresseRueArriver { get; set; }

        [Required(ErrorMessage = "Le code postal d'arrivée est obligatoire.")]
        [StringLength(6, ErrorMessage = "Le code postal ne peut pas dépasser 6 caractères.")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Le code postal d'arrivée doit être composé de 5 chiffres.")]
        public string AdresseCpArriver { get; set; }

        [Required(ErrorMessage = "Le nom de la ville d'arrivée est obligatoire.")]
        [StringLength(60, ErrorMessage = "Le nom de la ville ne peut pas dépasser 60 caractères.")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\s\-']+$", ErrorMessage = "Le nom de la ville d'arrivée contient des caractères invalides.")]
        public string AdresseVilleArriver { get; set; }

        [Required(ErrorMessage = "Le prix du covoiturage est obligatoire.")]
        public double CovoitPrix { get; set; }

        [Required(ErrorMessage = "L'heure de départ est obligatoire.")]
        public TimeOnly CovoitDep { get; set; }

        [Required(ErrorMessage = "L'heure d'arrivée est obligatoire.")]
        public TimeOnly CovoitArr { get; set; }

        [Required(ErrorMessage = "Le champ fumeur est obligatoire.")]
        public bool CovoitFumeur { get; set; }

        [Required(ErrorMessage = "Le champ animaux est obligatoire.")]
        public bool CovoitAnimaux { get; set; }

        [Required(ErrorMessage = "Le champ musique est obligatoire.")]
        public bool CovoitMusique { get; set; }
    }
}
