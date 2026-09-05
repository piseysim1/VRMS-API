namespace vrms_api.Models
{
    public class LocationModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string Note { get; set; }
    }

    public class LocationAddModel : LocationModel
    {

    }
    public class LocationListModel : LocationModel
    {
        public int Id { get; set; }
    }
    public class LocationEditModel : LocationListModel
    {
       
    }
    public class LocationViewModel : LocationListModel
    {

    }
}
