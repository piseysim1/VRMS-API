namespace vrms_api.Models
{
    public class RateModel
    {
        public long ModelId { get; set; }
        public long RateTypeId { get; set; }
        public long Price { get; set; }
    }
    public class RateAddModel : RateModel
    {
    }
    public class RateListModel : RateModel
    {
        public long Id { get; set; }
    }
    public class RateEditModel : RateListModel
    {

    }
    public class RateViewModel : RateListModel
    {
    }
}
