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
        public string ImageUrl { get; set; }
    }
    public class VehicleAddModel : VehicleModel
    {
    }
    public class VehicleListModel : VehicleModel
    {
        public long Id { get; set; }
        public string ModelName { get; set; }
        public string BrandName { get; set; }
        public long RateId { get; set; }
        public string LocationName { get; set; }
        public string StatusName { get; set; }
        public string ColorName { get; set; }
       
        public List<VehicleRateListModel> Rates { get; set; }
    }
    public class VehicleRateListModel
    {
        public long RateTypeId { get; set; }
        public string RateTypeName { get; set; }
        public long Price { get; set; }

    }
    public class VehicleEditModel : VehicleListModel
    {
    }
    public class VehicleViewModel : VehicleListModel
    {

    }
}

