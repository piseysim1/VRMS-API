namespace vrms_api.Models
{
    public class UserModel
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Profile { get; set; }
       

    }
    public class UserListModel :UserModel 
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public long VehicleId { get; set; }
        public long RentalId { get; set; }
        public string RoleName { get; set; }

    }
    public class UserAddModel : UserModel 
    {
        public long RoleId { get; set; }
        public string Password { get; set; }
    }
    public class UserEditModel: UserModel
    {
        public long Id { get; set; }
    }
    public class UserViewModel : UserModel 
    {
        public long Id { get; set; }
        public long VehicleId { get; set; }
        

    }

}
