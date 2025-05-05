using System.ComponentModel.DataAnnotations;

namespace BackOnWay.Dtos.Visiteur
{
    public class ContactDto
    {
        [Required]
        public string AdresseEmail { get; set; }

        [Required]
        public string Nom { get; set; }

        [Required]
        public string Telephone { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public string Titre { get; set; }
    }
}
