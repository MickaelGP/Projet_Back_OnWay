namespace BackOnWay.Dtos.Employe
{
    public class PlainteByIdDto
    {
        public int PlainteId { get; set; }
        public string PlainteDescription { get; set; }
        public string PlainteSatut { get; set; }
        public string UtilNom { get; set; }
        public string UtilEmail { get; set; }
        public DateOnly ResaDate { get; set; }
    }
}
