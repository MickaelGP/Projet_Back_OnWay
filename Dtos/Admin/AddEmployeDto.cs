using System.ComponentModel.DataAnnotations;

namespace BackOnWay.Dtos.Admin
{
    public class AddEmployeDto
    {
        [Required(ErrorMessage = "Le nom est requis")]
        [StringLength(50, MinimumLength =5 ,  ErrorMessage = "Le nombre de caractère doit être compris entre 5 et  50 caractères.")]
        public string UtilNom { get; set; }

        [Required(ErrorMessage = "Le prénom est requis")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Le nombre de caractère doit être compris entre 5 et  50 caractères.")]
        public string UtilPrenom { get; set; }

        [Required(ErrorMessage = "L'adresse email est requis.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Format invalide")]
        public string UtilEmail { get; set; }

        [Required(ErrorMessage = "Le mot de passe est requis.")]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-z^A-Z^0-9]).{8,}$", ErrorMessage = "Format invalide")]
        public string UtilMdp { get; set; }

        public DateOnly UtilNaissance { get; set; }

        [Required(ErrorMessage = "Le genre est requis.")]
        [StringLength(1, ErrorMessage = "Le nombre de caractère ne doit pas dépasser 1.")]
        public string UtilGenre { get; set; }
    }
}
