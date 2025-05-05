namespace BackOnWay.Dtos.Visiteur
{
    public class RechercheCovoitDto
    {
        public int CovoitId { get; set; }

        public DateOnly CovoitDate { get; set; }

        public double CovoitPrix { get; set; }

        public TimeOnly CovoitDepart { get; set; }

        public TimeOnly CovoitArriver { get; set; }

        public string VilleDepart { get; set; }

        public string VilleArriver { get; set; }
    }
}
