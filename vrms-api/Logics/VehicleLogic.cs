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
            var search = param.GetSearchFilter()?.ToLower();

            var q = from qV in qVehicle
                    join m in _db.Models.Where(x => x.Active) on qV.ModelId equals m.Id
                    join b in _db.Brands.Where(x => x.Active) on qV.BrandId equals b.Id
                    join l in _db.Locations.Where(x => x.Active) on qV.LocationId equals l.Id
                    
                    join s in _db.LookUpItems.Where(x => x.Active && x.LookUpTypeId == (long)LookUpTypes.VehicleStatus) on qV.StatusId equals s.Id
                    join c in _db.LookUpItems.Where(x => x.Active && x.LookUpTypeId == (long)LookUpTypes.Color) on qV.ColorId equals c.Id

                    select new VehicleListModel
                    {
                        Id = qV.Id,
                        ModelId = qV.ModelId,
                        LicensePlate = qV.LicensePlate,
                        BrandId = qV.BrandId,
                        LocationId = qV.LocationId,
                        Rates = _db.Rates.Where(x => x.Active && x.ModelId == qV.ModelId).Select(x => new VehicleRateListModel
                        {
                            RateTypeId = x.RateTypeId,
                            RateTypeName = _db.LookUpItems.FirstOrDefault(l => l.Active && l.LookUpTypeId == (long)LookUpTypes.RateType && l.Id == x.RateTypeId).NameEn,
                            Price =(long)x.Price
                        }).ToList(),
                        ModelName = m.Name,
                        BrandName = b.Name,
                        LocationName = l.Name,
                        StatusId = qV.StatusId,
                        StatusName = s.NameEn,
                        ColorId = qV.ColorId,
                        ColorName = c.NameEn,
                        ImageUrl = qV.ImageUrl
                    };
            if (!string.IsNullOrEmpty(search))
            {
                q = q.Where(x => x.ModelName.ToLower().Contains(search) ||
                                 x.LicensePlate.ToLower().Contains(search) ||
                                 x.BrandName.ToLower().Contains(search));
            }

            await param.UpdateCountAsync(q);

            var list = await q.ToFilterView(param).ToListAsync();

            return new SearchResult<VehicleListModel, QueryParamModel>
            {
                Results = list,
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
            if (entity.StatusId == 0) 
            {
                entity.StatusId = (long)VehicleStatus.Available;
            }
                
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
