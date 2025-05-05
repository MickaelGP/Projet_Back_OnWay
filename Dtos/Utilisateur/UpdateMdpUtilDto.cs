namespace BackOnWay.Dtos.Utilisateur
{
    public class UpdateMdpUtilDto
    {
        public int UtilId { get; set; }

        public string UtilMdp { get; set; }

        public string OldMdp { get; set; }
    }
}
