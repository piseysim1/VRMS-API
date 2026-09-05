namespace vrms_api.Domains
{
    public class VrmsDomain
    {
        public class Brand : ShareDomain
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public string Note { get; set; }
        }

        public class PaymentMethod : ShareDomain
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public string Note { get; set; }
            public string QrImage { get; set; }
            public string ImageLogo { get; set; }
            public int Ordering { get; set; }
        }
        public class  Model:ShareDomain
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public string Note { get; set; }

        }
        public class Location:ShareDomain
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public string Detail { get; set; }
            public string Note { get; set; }
        } 
        public class Vehicle: ShareDomain
        {
            public string LicensePlate { get; set; }
            public long BrandId { get; set; }
            public long ModelId { get; set; }
            public long LocationId { get; set; }
            public long ColorId { get; set; }
            public long  StatusId { get; set; }
        }
        public class  Booking :ShareDomain
        {
            public string BookingNo { get; set; }
            public long UserId { get; set; }
            public long BusinessSourceId { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public long Total { get; set; }

        }
        public class  BookingDetail: ShareDomain
        {
            public long BookingId { get; set; }
            public long VehicleId { get; set; }
            public String Description { get; set; }
            public long Qty { get; set; }
            public long UnitPrice { get; set; }
            public long SubTotal { get; set; }
        }
        public class  Rate: ShareDomain 
        {
            public long ModelId { get; set; }
            public long RateTypeId { get; set; }
            public long Price { get; set; }
        }
        public class LookUpType:ShareDomain
        {
            public string Name { get; set; }
            public string NameEn { get; set; }

        }
        public class  LookUpItem: ShareDomain
        {
            public long LookUpTypeId { get; set; }
            public string Name { get; set; }
            public string NameEn { get; set; }
            public long Ordering { get; set; }
            public string Note { get; set; }
            public string ImageUrl { get; set; }

        }
        public class AutoNumberModel : ShareDomain
        {
            public string Name { get; set; }
            public string Format { get; set; }
            public string Note { get; set; }
        }

    }
}
