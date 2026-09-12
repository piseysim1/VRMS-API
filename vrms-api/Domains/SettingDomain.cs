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
            public long LookupTypeId { get; set; }
            public string Name { get; set; }
            public string NameEn { get; set; }
            public int Ordering { get; set; }
            public string Note { get; set; }
            public string ImageUrl { get; set; }
        }
    }
}
