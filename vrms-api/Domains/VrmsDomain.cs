namespace vrms_api.Domains
{
    public class VrmsDomain
    {
        public class Brand : ShareDomain
        {
            public String Code { get; set; }
            public String Name { get; set; }
            public String Note { get; set; }
        }

        public class PaymentMethod : ShareDomain
        {
            public String Code { get; set; }
            public String Name { get; set; }
            public String Note { get; set; }
            public String Image { get; set; }
            public int Ordering { get; set; }
        }
        public class  Model:ShareDomain
        {
            public String Code { get; set; }
            public String Name { get; set; }
            public String Note { get; set; }

        }
        public class Location:ShareDomain
        {
            public String Code { get; set; }
            public String Name { get; set; }
            public String Detail { get; set; }
            public String Note { get; set; }
        }
        public class VehicleType:ShareDomain
        {
            
        }
    }
}
