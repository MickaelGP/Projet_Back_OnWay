using System.ComponentModel.DataAnnotations;

namespace BackOnWay.Models
{
    public class Roles
    {
        /// <summary>
        /// Id du rôle
        /// </summary>
        [Key]
        public int RoleId { get; set; }

        /// <summary>
        /// Nom du rôle
        /// </summary>
        [Required(ErrorMessage = "Le nom du rôle est requis.")]
        [StringLength(15, ErrorMessage = "Le champ doit comporter 15 caractères au maximum")]
        public string RoleLabel { get; set; }

        public Roles() { }

    }
}
