namespace vrms_api.Models
{
    public class BrandModel
    {
        public String Code { get; set; }
        public String Name { get; set; }
        public String Note { get; set; }
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
    public class  BrandRemoveModel: BrandEditModel
    {
        
    }
    public class  BrandViewModel: BrandListModel
    {
        
    }
}
