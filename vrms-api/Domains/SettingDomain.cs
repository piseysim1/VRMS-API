namespace vrms_api.Domains
{
    public class SettingDomain
    {
        public class LookUpType: ShareDomain 
        { 
            public string Name { get; set; }
            public string NameEn { get; set; }
           
        }
        public class LookUpItem : ShareDomain
        {
            public long LookUpTypeId { get; set; }
            public string Name { get; set; }
            public string NameEn { get; set; }
            public int Ordering { get; set; }
            public string Color { get; set; }
            public string Note { get; set; }
            public string ImageUrl { get; set; }
        }
    }
}
