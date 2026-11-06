namespace vrms_api.Models
{
    public class RegisterModel: LoginModel
    {
        //public long UserId { get; set; }
        //public long RoleId { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
   
    }
    public class AuthResponseModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

    }
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class RefreshTokenModel
    {
        public string RefreshToken { get; set; }
    }
}
