namespace vrms_api.Models
{
    public class PaymentMethodModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public string QrImage { get; set; }
        public string ImageLogo { get; set; }
        public int Ordering { get; set; }
    }
    public class PaymentMethodAddModel : PaymentMethodModel
    {
    }
    public class PaymentMethodListModel : PaymentMethodAddModel
    {
        public long Id { get; set; }
    }
    public class PaymentMethodEditModel : PaymentMethodListModel
    {

    }
    public class PaymentMethodViewModel : PaymentMethodEditModel
    {

    }
}
