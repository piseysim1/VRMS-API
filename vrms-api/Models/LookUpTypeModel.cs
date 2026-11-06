namespace vrms_api.Models
{
    public class LookUpTypeModel
    {
        public string Name { get; set; }
        
    }
    public class LookUpTypeAddModel: LookUpTypeModel
    {

    }
    public class LookUpTypeEditModel: LookUpTypeModel
    {
        public long Id { get; set; }
    }
    public class LookUpTypeViewModel: LookUpTypeModel
    {

    }
    public class LookUpTypeListModel: LookUpTypeModel
    {

    }
}
