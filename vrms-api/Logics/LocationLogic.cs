
using Microsoft.EntityFrameworkCore;
using vrms_api.Extensions;
using vrms_api.Models;
using static vrms_api.Domains.VrmsDomain;
using static vrms_api.Models.ShareModel;

namespace vrms_api.Logics
{
    public class LocationLogic(IServiceProvider provider) : ShareLogic(provider)
    {
        public async Task<LocationViewModel>FindAsync(long id)
        {
            var entity = await _db.Locations.FirstOrDefaultAsync(x => x.Id == id && x.Active);
            if (entity == null) 
            { 
                throw new Exception("Location not found");
            }
            return _mapper.Map<LocationViewModel>(entity);
        }
        public async Task<SearchResult<LocationListModel , QueryParamModel>> SearchAsync(QueryParamModel param)
        {
            var q = _db.Locations.Where(x => x.Active);
            var search = param.GetSearchFilter().ToLower();
            if (!string.IsNullOrEmpty(search)) 
            {
                q = q.Where(x => x.Name.ToLower().Contains(search));
            }
            await param.UpdateCountAsync(q);
            q = q.ToFilterView(param);
            return new SearchResult<LocationListModel, QueryParamModel>
            {
                Results = _mapper.Map<List<LocationListModel>>(await q.ToListAsync()),
                Param= param
            };
        }
        public async Task<LocationViewModel>AddLocationAsync(LocationAddModel model)
        {
            var entity = _mapper.Map<Location>(model);
            entity.CreatedAt = DateTime.Now;    
            await _db.Locations.AddAsync(entity);
            await _db.SaveChangesAsync();
            return await FindAsync(entity.Id);
        }
        public async Task<LocationViewModel>EditLocationAsync(LocationEditModel model)
        {
            var entity = await _db.Locations.FirstOrDefaultAsync(x => x.Id == model.Id && x.Active);
            if(entity == null)
            {
                throw new Exception("Location not found");
            }
            _mapper.Map(model, entity);
            entity.UpdatedAt = DateTime.Now;
            await _db.SaveChangesAsync();
            return await FindAsync(entity.Id);
        }
        public async Task<LocationViewModel>RemoveLocationAsync(RemoveModel model)
        {
            var entity = await _db.Locations.FirstOrDefaultAsync(x => x.Id == model.Id && x.Active);
            if(entity == null)
            {
                throw new Exception("Location not found");
            }
            entity.Active = false;
            entity.UpdatedAt = DateTime.Now;
            await _db.SaveChangesAsync();
            return await FindAsync(entity.Id);
        }
    }
}
