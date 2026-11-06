using System.Collections.Generic;

namespace vrms_api.Models
{
    public class BookingModel
    {
        public string BookingNo { get; set; }
        public long UserId { get; set; }
        public long? LocationId { get; set; }
        public long? PaymentMethodId { get; set; }
        public string IdentityCardUrl { get; set; }
        public string VehicleDocumentUrl { get; set; }
        public string PaymentProofUrl { get; set; }
        public string VehicleRegistrationUrl { get; set; }
        public long StatusId { get; set; }
        public long BusinessSourceId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Decimal Total { get; set; }
    }
    public class BookingAddModel: BookingModel  
    {
        public List<BookingDetailAddModel> BookingDetails { get; set; } = new();
    }

    public class BookingEditModel : BookingModel
    {
        public long Id { get; set; }
        
    }

    public class BookingListModel : BookingModel
    {
        public long Id { get; set; }
        public string LocationName { get; set; }
        public string PaymentMethodName { get; set; }
        public string StatusName { get; set; }
        public long BookingId { get; set; }
        public string BusinessSourceName { get; set; }
        public string Username { get; set; }
    }

    public class BookingViewModel : BookingModel
    {
        public long Id { get; set; }
        public long BookingId { get; set; }
        public List<BookingDetailViewModel> BookingDetails { get; set; } = new();
        public string BusinessSourceName { get; set; }
        public string Username { get; set; }
    }

    public class BookingStatusModel { 
        public long Id { get; set; } 
        public long StatusId { get; set; } 
    }

}
