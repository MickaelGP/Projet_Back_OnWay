namespace BackOnWay.Dtos.Admin
{
    public class UtilisateurDto
    {
        public int UtilId { get; set; }
        public string UtilNom { get; set; }
        public string UtilPseudo { get; set; }
        public string UtilEmail { get; set; }
        public bool UtilSuspendu { get; set; }
        public string RoleLabel { get; set; }
    }
}
