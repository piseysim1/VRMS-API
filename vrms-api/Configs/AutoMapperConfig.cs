using AutoMapper;
using vrms_api.Models;
using static vrms_api.Domains.VrmsDomain;


namespace vrms_api.Configs
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Brand, BrandListModel>().ReverseMap();
            CreateMap<Brand, BrandAddModel>().ReverseMap();
            CreateMap<Brand, BrandEditModel>().ReverseMap();
            CreateMap<Brand, BrandViewModel>().ReverseMap();

            CreateMap<Model, ModelVehicleAddModel>().ReverseMap();
            CreateMap<Model, ModelVehicleEditModel>().ReverseMap();
            CreateMap<Model, ModelVehicleViewModel>().ReverseMap();
            CreateMap<Model, ModelVehicleListModel>().ReverseMap();

            CreateMap<Location, LocationAddModel>().ReverseMap();
            CreateMap<Location, LocationEditModel>().ReverseMap();
            CreateMap<Location, LocationViewModel>().ReverseMap();
            CreateMap<Location, LocationListModel>().ReverseMap();

            CreateMap<PaymentMethod, PaymentMethodAddModel>().ReverseMap();
            CreateMap<PaymentMethod, PaymentMethodEditModel>().ReverseMap();
            CreateMap<PaymentMethod, PaymentMethodViewModel>().ReverseMap();
            CreateMap<PaymentMethod, PaymentMethodListModel>().ReverseMap();


            CreateMap<Rate, RateAddModel>().ReverseMap();
            CreateMap<Rate, RateEditModel>().ReverseMap();
            CreateMap<Rate, RateListModel>().ReverseMap();
            CreateMap<Rate, RateViewModel>().ReverseMap();

            CreateMap<Vehicle, VehicleAddModel>().ReverseMap();
            CreateMap<Vehicle, VehicleEditModel>().ReverseMap();
            CreateMap<Vehicle, VehicleListModel>().ReverseMap();
            CreateMap<Vehicle, VehicleViewModel>().ReverseMap();


        }
   
    }
}
