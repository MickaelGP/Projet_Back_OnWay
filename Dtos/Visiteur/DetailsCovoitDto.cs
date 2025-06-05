namespace BackOnWay.Dtos.Visiteur
{
    public class DetailsCovoitDto
    {
        public int CovoitId { get; set; }

        public double CovoitPrix { get; set; }

        public DateOnly CovoitDate { get; set; }

        public TimeOnly CovoitDep { get; set; }

        public TimeOnly CovoitArr { get; set; }

        public bool CovoitAnimaux { get; set; }

        public bool CovoitFumeur { get; set; }

        public bool CovoitMusique { get; set; }

        public int DepartNum { get; set; }

        public string DepartRue { get; set; }

        public string DepartVille { get; set; }

        public int ArriverNum { get; set; }

        public string ArriveRue { get; set; }

        public string ArriveVille { get; set; }

        public string VoitEnergie { get; set; }

        public string VoitCouleur { get; set; }

        public string VoitModele { get; set; }

        public string VoitMarque { get; set; }

        public int ConducId { get; set; }

        public string ConducPseudo { get; set; }

        public double Note { get; set; }

        public int NbCom { get; set; }
    }
}
