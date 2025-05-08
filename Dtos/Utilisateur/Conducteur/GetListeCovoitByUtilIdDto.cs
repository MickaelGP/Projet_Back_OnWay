namespace BackOnWay.Dtos.Utilisateur.Conducteur
{
    public class GetListeCovoitByUtilIdDto
    {
        public int CovoitId { get; set; }

        public DateOnly CovoitDate { get; set; }

        public TimeOnly CovoitDep { get; set; }

        public TimeOnly CovoitArr { get; set; }

        public string? DepartVille { get; set; }

        public string? ArriveVille { get; set; }
    }
}
