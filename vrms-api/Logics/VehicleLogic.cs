using vrms_api.Models;
using static vrms_api.Domains.VrmsDomain;

namespace vrms_api.Logics
{
    public class VehicleLogic(IServiceProvider provider):ShareLogic(provider)
    {
        public async Task<VehicleViewModel> AddAsync(VehicleAddModel model)
        {
            var entity =  _mapper.Map<Vehicle>(model);
            entity.CreatedAt = DateTime.Now;
            return _mapper.Map<VehicleViewModel>(entity);
        }
    }
}
