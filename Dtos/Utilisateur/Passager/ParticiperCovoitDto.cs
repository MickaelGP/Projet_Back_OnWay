using System.ComponentModel.DataAnnotations;
using BackOnWay.Models;

namespace BackOnWay.Dtos.Utilisateur.Passager
{
    public class ParticiperCovoitDto
    {     
        public string ResaStatut { get; set; }

       
        [Required(ErrorMessage = "Le nombre de siége est requis.")]
        public byte ResaNbSiege { get; set; }

      
        [Required(ErrorMessage = "La date est requise.")]
        public DateOnly ResaDate { get; set; }

        [Required]
        public int ResaUtil { get; set; }

        [Required]
        public int ResaCovoit { get; set; }
    }
}
