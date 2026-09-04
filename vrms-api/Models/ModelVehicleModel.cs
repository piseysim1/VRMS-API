namespace vrms_api.Models
{
    public class ModelVehicleModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }

    }
    public class ModelVehicleAddModel : ModelVehicleModel
    {
    }
    public class ModelVehicleListModel : ModelVehicleAddModel
    {
        public long Id { get; set; }
    }
    public class ModelVehicleEditModel : ModelVehicleListModel
    {
       
    }
    public class ModelVehicleViewModel : ModelVehicleEditModel
    {
        
    }
}
