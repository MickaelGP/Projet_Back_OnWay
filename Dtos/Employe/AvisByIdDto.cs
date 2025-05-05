namespace BackOnWay.Dtos.Employe
{
    public class AvisByIdDto
    {
        public int AvisId { get; set; }

        public string AvisTitre { get; set; }

        public string AvisCom { get; set; }

        public byte AvisNote { get; set; }

        public string AvisStatut { get; set; }

        public string NomExpediteur { get; set; }

        public string NomDestinataire { get; set; }
    }
}
