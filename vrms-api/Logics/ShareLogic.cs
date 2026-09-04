using AutoMapper;

namespace vrms_api.Logics
{
    public class ShareLogic
    {
        private readonly IConfiguration _config;
        protected readonly VrmsDbContext _db;
        protected readonly IMapper _mapper;
        protected readonly IServiceProvider _provider;
        public ShareLogic(IServiceProvider provider)
        {
            _provider = provider;
            _config = provider.GetRequiredService<IConfiguration>();
            _db = provider.GetRequiredService<VrmsDbContext>();
            _mapper = provider.GetRequiredService<IMapper>();
        }
    }
}
