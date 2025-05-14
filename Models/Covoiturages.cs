using System.ComponentModel.DataAnnotations;

namespace BackOnWay.Models
{
    public class Covoiturages
    {
        /// <summary>
        /// Identifiant du covoiturage
        /// </summary>
        [Key]
        public int CovoitId { get; set; }

        /// <summary>
        /// Prix du covoiturage 
        /// </summary>
        public double CovoitPrix { get; set; }

        /// <summary>
        /// Date de départ du covoiturage
        /// </summary>
        public DateOnly CovoitDate { get; set; }

        /// <summary>
        /// Heure de départ du covoiturage
        /// </summary>
        public TimeOnly CovoitDep { get; set; }

        /// <summary>
        /// Heure d'arrivé du covoiturage
        /// </summary>
        public TimeOnly CovoitArr { get; set; }

        /// <summary>
        /// Etat du covoiturage ( En attente )
        /// </summary>
        public string CovoitStatut { get; set; }
        /// <summary>
        /// Preference
        /// </summary>
        public bool CovoitMusique { get; set; }
        /// <summary>
        /// Preference
        /// </summary>
        public bool CovoitFumeur { get; set; }
        /// <summary>
        /// Preference
        /// </summary>
        public bool CovoitAnimaux { get; set; }
        /// <summary>
        /// Identifiant de la voiture
        /// </summary>
        public int CovoitVoiture { get; set; }
        public Voitures Voiture { get; set; }
        /// <summary>
        /// Identifiant de l'adresse de départ
        /// </summary>
        public int DepAdresse { get; set; }
        public Adresses DepartAdresse { get; set; }

        /// <summary>
        /// Identifiant de l'adresse d'arrivée
        /// </summary>
        public int ArrAdresse { get; set; }
        public Adresses ArriveAdresse { get; set; }

        public Covoiturages() { }
    }
}
