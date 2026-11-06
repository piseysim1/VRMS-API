namespace vrms_api.Models
{
    public class BookingDetailModel
    {
        
        public long VehicleId { get; set; }
        public String Description { get; set; }
        public long Qty { get; set; }
        public Decimal UnitPrice { get; set; }
        public Decimal SubTotal { get; set; }
    }
    public class BookingDetailAddModel : BookingDetailModel
    {
       
    }
    public class BookingDetailListModel : BookingDetailModel
    {
        public long Id { get; set; }
        public long BookingId { get; set; }
    }

    public class BookingDetailEditModel : BookingDetailModel
    {
        public long Id { get; set; }
        public long BookingId { get; set; }
    }

    public class BookingDetailViewModel : BookingDetailModel
    {
        public long Id { get; set; }
        public long BookingId { get; set; }
    }
}
