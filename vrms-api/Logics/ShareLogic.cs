using AutoMapper;
using Newtonsoft.Json;
using static vrms_api.Models.ShareModel;

namespace vrms_api.Logics
{
    public class ShareLogic
    {
        protected readonly IConfiguration _config;
        protected readonly VrmsDbContext _db;
        protected readonly IMapper _mapper;
        protected readonly IServiceProvider _provider;
        protected readonly HttpContext _httpContext;
        public ShareLogic(IServiceProvider provider)
        {
            _provider = provider;
            _httpContext = provider.GetService<IHttpContextAccessor>().HttpContext;
            _config = provider.GetRequiredService<IConfiguration>();
            _db = provider.GetRequiredService<VrmsDbContext>();
            _mapper = provider.GetRequiredService<IMapper>();
        }
       
        
    }
}
