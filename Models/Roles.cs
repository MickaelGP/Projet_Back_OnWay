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
        public string RoleLabel { get; set; }

        public Roles() { }

    }
}
