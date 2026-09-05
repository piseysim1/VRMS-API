namespace vrms_api.Models
{
    public class VehicleModel
    {
        public string LicensePlate { get; set; }
        public long BrandId { get; set; }
        public long ModelId { get; set; }
        public long LocationId { get; set; }
        public long ColorId { get; set; }
        public long StatusId { get; set; }
    }
    public class VehicleAddModel : VehicleModel
    {
    }
    public class VehicleListModel : VehicleModel
    {
        public long Id { get; set; }
    }
    public class VehicleEditModel : VehicleListModel
    {
    }
    public class VehicleViewModel : VehicleListModel
    {

    }
}

