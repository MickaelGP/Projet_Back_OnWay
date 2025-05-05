using System.ComponentModel.DataAnnotations;

namespace BackOnWay.Dtos.Auth
{
    public class ConnexionDto
    {
        [Required(ErrorMessage = "L'adresse email est requise.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Format invalide")]
        public string UtilEmail { get; set; }

        [Required(ErrorMessage = "Le mot de passe est requis.")]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-z^A-Z^0-9]).{8,}$", ErrorMessage = "Format invalide")]
        public string UtilMdp { get; set; }
    }
}
