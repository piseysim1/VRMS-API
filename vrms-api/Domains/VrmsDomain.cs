using System.ComponentModel.DataAnnotations;

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
            public string ImageUrl { get; set; }
        }
        public class  Booking :ShareDomain
        {
            public string BookingNo { get; set; }
            public long UserId { get; set; }
            public long? LocationId { get; set; }
            public long? PaymentMethodId { get; set; }
            public string IdentityCardUrl { get; set; }
            public string VehicleDocumentUrl { get; set; }
            public string PaymentProofUrl { get; set; }
            public string VehicleRegistrationUrl { get; set; }
            public long BusinessSourceId { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public decimal Total { get; set; }
            public long StatusId { get; set; }

            public ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

        }
        public class  BookingDetail: ShareDomain
        {
            public long BookingId { get; set; }
            public Booking Booking { get; set; }
            public long VehicleId { get; set; }
            public string Description { get; set; }
            public long Qty { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal SubTotal { get; set; }
           
        }
        public class Rate : ShareDomain 
        {
            public string Code { get; set; }
            public long ModelId { get; set; }
            public long RateTypeId { get; set; }
            public Decimal Price { get; set; }
        }
        public class AutoNumberModel : ShareDomain
        {
            public string Name { get; set; }
            public string Format { get; set; }
            public string Note { get; set; }
        }
        public  class User: ShareDomain
        {

            public string Username { get; set; }
            public string FullName { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string PasswordHash { get; set; }
            public string Profile { get; set; }

        }
        public class LoginSession : ShareDomain
        {
            public long UserId { get; set; }
            public string Token { get; set; }
            public string IpAddress { get; set; }
            public string DeviceName { get; set; }
            public string Browser { get; set; }
            public DateTime ExpiresAt { get; set; }
            public DateTime? LogOutDate { get; set; }
            /// <summary>
            /// Set to true on Logout
            /// </summary>
            public bool IsRevoked { get; set; }
        }
        public class Role: ShareDomain
        {
            public string Name { get; set; }
            public string Note { get; set; }

        }
        public class UserRole: ShareDomain
        {
            public long UserId { get; set; }
            public long RoleId { get; set; }
        }

    }
}
