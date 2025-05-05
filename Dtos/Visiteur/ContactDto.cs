using System.ComponentModel.DataAnnotations;

namespace BackOnWay.Dtos.Visiteur
{
    public class ContactDto
    {
        [Required(ErrorMessage = "L'adresse email est requise.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Format invalide")]
        public string AdresseEmail { get; set; }

        [Required(ErrorMessage = "Le nom est requis.")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le téléphone est requis.")]
        [RegularExpression(@"^((\+|00)33\s?|0)[67](\s?\d{2}){4}$", ErrorMessage = "Format invalide")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "Le message est requis.")]
        [StringLength(255, MinimumLength = 10, ErrorMessage ="Le message doit être compris entre 10 et 255 carractéres")]
        public string Message { get; set; }

        [Required(ErrorMessage = "Le titre est requis.")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Le message doit être compris entre 5 et 30 carractéres")]
        public string Titre { get; set; }
    }
}
