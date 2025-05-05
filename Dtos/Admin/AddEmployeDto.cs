using System.ComponentModel.DataAnnotations;

namespace BackOnWay.Dtos.Admin
{
    public class AddEmployeDto
    {
        [Required(ErrorMessage = "Le nom est requis")]
        public string UtilNom { get; set; }

        [Required(ErrorMessage = "Le prénom est requis")]
        public string UtilPrenom { get; set; }

        [Required(ErrorMessage = "L'adresse email est requis.")]
        public string UtilEmail { get; set; }

        [Required(ErrorMessage = "Le mot de passe est requis.")]
        public string UtilMdp { get; set; }
        public DateOnly UtilNaissance { get; set; }

        [Required(ErrorMessage = "Le genre est requis.")]
        [StringLength(1, ErrorMessage = "Le nombre de caractère ne doit pas dépasser 1.")]
        public string UtilGenre { get; set; }
    }
}
