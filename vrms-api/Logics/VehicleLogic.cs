using Microsoft.EntityFrameworkCore;
using vrms_api.Domains;
using vrms_api.Extensions;
using vrms_api.Models;
using static vrms_api.Domains.VrmsDomain;
using static vrms_api.Models.ShareModel;

namespace vrms_api.Logics
{
    public class VehicleLogic(IServiceProvider provider):ShareLogic(provider)
    {
        public async Task<SearchResult<VehicleListModel, QueryParamModel>> SearchAsync(QueryParamModel param)
        {
            var qVehicle = _db.Vehicles.Where(x => x.Active);
            var search = param.GetSearchFilter().ToLower();

            var q = from qV in qVehicle
                    join m in _db.Models.Where(x => x.Active) on qV.ModelId equals m.Id
                    join b in _db.Brands.Where(x => x.Active) on qV.BrandId equals b.Id
                    join l in _db.Locations.Where(x => x.Active) on qV.LocationId equals l.Id
                    join r in _db.Rates.Where(x => x.Active) on m.Id equals r.ModelId
                    join s in _db.LookUpItems.Where(x => x.Active && x.LookUpTypeId == (long)LookupTypes.VehicleStatus) on qV.StatusId equals s.Id
                    join c in _db.LookUpItems.Where(x => x.Active && x.LookUpTypeId == (long)LookupTypes.Color) on qV.ColorId equals c.Id

                    select new VehicleListModel
                    {
                        Id = qV.Id,
                        ModelId = qV.ModelId,
                        LicensePlate= qV.LicensePlate,
                        BrandId = qV.BrandId,
                        LocationId = qV.LocationId,
                        RateId = r.Id,
                        ModelName = m.Name,
                        BrandName = b.Name,
                        LocationName = l.Name,
                        StatusId = qV.StatusId,
                        StatusName = s.Name,
                        ColorName = c.Name,
                    };
            if (!string.IsNullOrEmpty(search))
            {
                q = q.Where(x => x.ModelName.ToLower().Contains(search));

            }

            return new SearchResult<VehicleListModel, QueryParamModel>
            {
                Results = _mapper.Map<List<VehicleListModel>>(await q.ToFilterView(param).ToListAsync()),
                Param = param
            };
        }

        public async Task<VehicleViewModel> FindAsync(long id)
        {
            var entity = await _db.Vehicles.FirstOrDefaultAsync(x => x.Id == id && x.Active);
            if(entity == null)
            {
                throw new Exception("Vehicle not found");
            }
            return _mapper.Map<VehicleViewModel>(entity);
        }
        public async Task<VehicleViewModel> AddAsync(VehicleAddModel model)
        {
            var entity =  _mapper.Map<Vehicle>(model);
            entity.CreatedAt = DateTime.Now;
            entity.StatusId = (long)VehicleStatus.Available;
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
            return await FindAsync(entity.Id);
        }
        public async Task<VehicleViewModel>EditAsync(VehicleEditModel model)
        {
            var entity = await _db.Vehicles.FirstOrDefaultAsync(x => x.Id == model.Id && x.Active);
            if (entity == null) 
            { 

            }
            entity.UpdatedAt = DateTime.Now;
            _mapper.Map(model, entity);
            await _db.SaveChangesAsync();
            return await FindAsync(entity.Id);
        }
        public async Task<VehicleViewModel>RemoveAsync (RemoveModel model)
        {
            var entity = await _db.Vehicles.FirstOrDefaultAsync(x => x.Id == model.Id && x.Active);
            if(entity == null)
            {
                throw new Exception("Vehicle not found");
            }
            entity.Active = false;
            entity.UpdatedAt = DateTime.Now;
            await _db.SaveChangesAsync();
            return await FindAsync(entity.Id);
        }
    }
}
