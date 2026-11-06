using Microsoft.AspNetCore.Mvc;
using vrms_api.Logics;
using vrms_api.Models;
using static vrms_api.Models.ShareModel;


namespace vrms_api.Controllers
{
    public class AuthController(IServiceProvider provider) : ShareController<AuthLogic>(provider)
    {
        [HttpPost("register")]
        public async Task<ApiResponse<string>> Register(RegisterModel model)
        {
            return await _logic.Register(model);
        }
        [HttpPost("login")]
        public async Task<ApiResponse<AuthResponseModel>> Login(LoginModel model)
        {
            return await _logic.Login(model);
        }
        [HttpPost("refresh")]
        public async Task<ApiResponse<AuthResponseModel>> Refresh(RefreshTokenModel model)
        {
            return await _logic.Refresh(model);
        }
        [HttpPost("logout")]
        public async Task<ApiResponse<string>> Logout(RefreshTokenModel model)
        {
            return await _logic.Logout(model);
        }
    }
    
}
