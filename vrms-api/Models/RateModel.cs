using vrms_api.Domains;

namespace vrms_api.Models
{
    public class RateModel
    {
        public string Code { get; set; }
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
        public string ModelName { get; set; }
        public string RateTypeName { get; set; }
        public string RateTypeImage { get; set; }
    }
    public class RateEditModel : RateListModel
    {

    }
    public class RateViewModel : RateListModel
    {
    }
}
