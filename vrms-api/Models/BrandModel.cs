namespace vrms_api.Models
{
    public class BrandModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
    }
    public class BrandAddModel: BrandModel
    {

    }
    public class BrandListModel : BrandModel
    {
        public int Id { get; set; }
    }
    public class BrandEditModel : BrandListModel
    {
      
    }
    public class  BrandViewModel: BrandListModel
    {
        
    }
}
