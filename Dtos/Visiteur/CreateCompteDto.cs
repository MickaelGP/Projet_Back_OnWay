using System.ComponentModel.DataAnnotations;

namespace BackOnWay.Dtos.Visiteur
{
    public class CreateCompteDto
    {
        [Required(ErrorMessage = "Le pseudo est requise.")]
        [StringLength(30, MinimumLength =5, ErrorMessage = "Le nombre de caractère doit être compris entre 5 et 30 caractères.")]
        public string UtilPseudo { get; set; }

        [Required(ErrorMessage = "L'adresse email est requise.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Format invalide")]
        public string UtilEmail { get; set; }

        [Required(ErrorMessage = "Le mot de passe est requis.")]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-z^A-Z^0-9]).{8,}$", ErrorMessage = "Format invalide")]
        public string UtilMdp { get; set; }

        public DateOnly UtilNaissance { get; set; }

        [Required(ErrorMessage = "Le genre est requis.")]
        public string UtilGenre { get; set; }
    }
}
