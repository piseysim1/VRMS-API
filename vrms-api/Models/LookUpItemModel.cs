namespace vrms_api.Models
{
    public class LookUpItemModel
    {
        public long LookUpTypeId { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Color { get; set; }
        public long Ordering { get; set; }
        public string Note { get; set; }
        public string ImageUrl { get; set; }
    }
    public class LookUpItemAddModel : LookUpItemModel
    {

    }
    public class LookUpItemEditModel : LookUpItemViewModel
    {
    }

    public class LookUpItemListModel: LookUpItemViewModel
    {
       
    }

    public class LookUpItemViewModel: LookUpItemModel
    {
        public long Id { get; set; }
    }

}

