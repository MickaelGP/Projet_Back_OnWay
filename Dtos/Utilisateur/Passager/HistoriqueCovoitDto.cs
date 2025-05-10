namespace BackOnWay.Dtos.Utilisateur.Passager
{
    public class HistoriqueCovoitDto
    {
        public int CovoitId { get; set; }

        public DateOnly CovoitDate { get; set; }

        public TimeOnly CovoitDep { get; set; }

        public TimeOnly CovoitArr { get; set; }

        public string? Depart { get; set; }

        public string? Arriver { get; set; }

        public string CovoitStatut { get; set; }
    }
}
