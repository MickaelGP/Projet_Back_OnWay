namespace BackOnWay.Models
{
    public class Sessions
    {
        public int SessionId { get; set; }

        public DateTime SessionCreer { get; set; }

        public DateTime SessionFin {  get; set; }

        public bool SessionActive { get; set; }

        public string SessionToken { get; set; }

        public int SessionUtil { get; set; }
    }
}
