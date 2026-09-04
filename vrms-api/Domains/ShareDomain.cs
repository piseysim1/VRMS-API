namespace vrms_api.Domains
{
    public class ShareDomain
    {
        public long Id { get; set; }
        public bool Active { get; set; }= true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public long CreateBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
