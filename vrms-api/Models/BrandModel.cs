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
    public class BrandListModel : BrandAddModel
    {
        public int Id { get; set; }
    }
    public class BrandEditModel : BrandModel
    {
        public int Id { get; set; }
    }
    public class  BrandViewModel: BrandListModel
    {
        
    }
}
