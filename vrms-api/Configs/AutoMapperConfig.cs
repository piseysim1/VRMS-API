using AutoMapper;
using vrms_api.Models;
using static vrms_api.Domains.SettingDomain;
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

            CreateMap<User, RegisterModel>().ReverseMap();

            CreateMap<User, UserAddModel>().ReverseMap();
            CreateMap<User, UserEditModel>().ReverseMap();
            CreateMap<User, UserListModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();

            CreateMap<Role, RoleAddModel>().ReverseMap();
            CreateMap<Role, RoleEditModel>().ReverseMap();
            CreateMap<Role, RoleViewModel>().ReverseMap();
            CreateMap<Role, RoleListModel>().ReverseMap();

            CreateMap<Booking, BookingAddModel>().ReverseMap();
            CreateMap<Booking, BookingEditModel>().ReverseMap();
            CreateMap<Booking, BookingViewModel>().ReverseMap();
            CreateMap<Booking, BookingListModel>().ReverseMap();

            CreateMap<BookingDetail, BookingDetailListModel>().ReverseMap();
            CreateMap<BookingDetail, BookingDetailEditModel>().ReverseMap();
            CreateMap<BookingDetail, BookingDetailViewModel>().ReverseMap();
            CreateMap<BookingDetail, BookingDetailAddModel>().ReverseMap();
         

            CreateMap<LookUpItem, LookUpItemAddModel>().ReverseMap();
            CreateMap<LookUpItem, LookUpItemEditModel>().ReverseMap();
            CreateMap<LookUpItem, LookUpItemViewModel>().ReverseMap();
            CreateMap<LookUpItem, LookUpItemListModel>().ReverseMap();
           
            CreateMap<LookUpType, LookUpTypeListModel>().ReverseMap();
            CreateMap<LookUpType, LookUpTypeAddModel>().ReverseMap();
            CreateMap<LookUpType, LookUpTypeEditModel>().ReverseMap();
            CreateMap<LookUpType, LookUpTypeViewModel>().ReverseMap();
        }
   
    }
}
