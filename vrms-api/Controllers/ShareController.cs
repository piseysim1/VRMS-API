using Microsoft.AspNetCore.Mvc;
using vrms_api.Logics;

namespace vrms_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShareController<TLogic> : ControllerBase where TLogic : class
    {
        public readonly TLogic _logic;
        public readonly IServiceProvider _provider;
        public readonly VrmsDbContext _db;
        public ShareController(IServiceProvider provider)
        {
            _logic = provider.GetRequiredService <TLogic>();
            _provider = provider;
            _db = provider.GetRequiredService<VrmsDbContext>();
        }
    }

}
