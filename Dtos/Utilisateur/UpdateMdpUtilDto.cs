using System.ComponentModel.DataAnnotations;

namespace BackOnWay.Dtos.Utilisateur
{
    public class UpdateMdpUtilDto
    {
        [Required]
        public int UtilId { get; set; }

        [Required(ErrorMessage = "Le mot de passe est requis.")]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-z^A-Z^0-9]).{8,}$", ErrorMessage = "Format invalide")]
        public string UtilMdp { get; set; }

        [Required(ErrorMessage = "L'ancien mot de passe est requis.")]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-z^A-Z^0-9]).{8,}$", ErrorMessage = "Format invalide")]
        public string OldMdp { get; set; }
    }
}
