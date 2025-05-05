using System.ComponentModel.DataAnnotations;

namespace BackOnWay.Dtos.Utilisateur
{
    public class UtilProfilDto
    {
        [Required]
        public int UtilId { get; set; }

        [Required(ErrorMessage = "Le pseudo est requis")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Le nombre de caractère doit être compris entre 5 et  30 caractères.")]
        public string UtilPseudo { get; set; }

        [Required(ErrorMessage = "Le prénom est requis")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Le nombre de caractère doit être compris entre 5 et  50 caractères.")]
        public string UtilPrenom { get; set; }

        [Required(ErrorMessage = "Le nom est requis")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Le nombre de caractère doit être compris entre 5 et  50 caractères.")]
        public string UtilNom { get; set; }

        [Required(ErrorMessage = "La date est requise.")]
        public DateOnly UtilNaissance { get; set; }

        public short UtilCredit { get; set; }

        [Required(ErrorMessage = "L'adresse email est requis.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Format invalide")]
        public string UtilEmail { get; set; }

        [Required(ErrorMessage = "Le téléphone est requis.")]
        [RegularExpression(@"^((\+|00)33\s?|0)[67](\s?\d{2}){4}$", ErrorMessage = "Format invalide")]
        public string UtilTelephone { get; set; }

        [Required(ErrorMessage = "Le genre est requis.")]
        [StringLength(1, ErrorMessage = "Le nombre de caractère ne doit pas dépasser 1.")]
        public string UtilGenre { get; set; }
    }
}
