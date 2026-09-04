using Microsoft.EntityFrameworkCore;
using vrms_api.Extensions;
using vrms_api.Models;
using static vrms_api.Domains.VrmsDomain;
using static vrms_api.Models.ShareModel;

namespace vrms_api.Logics
{
    public class ModelLogic(IServiceProvider provider) : ShareLogic(provider)
    {
        public async Task<ModelVehicleViewModel>FindAsync(long id)
        {
            var entity =  await _db.Models.FirstOrDefaultAsync(x => x.Id == id && x.Active);
            if(entity == null)
            {
                throw new Exception("Model not found");
            }
            return _mapper.Map<ModelVehicleViewModel>(entity);
        }
        public async Task<SearchResult<ModelVehicleListModel , QueryParamModel>> SearchAsync(QueryParamModel param)
        {
            var q = _db.Models.Where(x => x.Active);
            var search = param.GetSearchFilter().ToLower();
            if (!string.IsNullOrEmpty(search)) 
            {
                q = q.Where(x => x.Name.ToLower().Contains(search));
            }
            await param.UpdateCountAsync(q);
            q = q.ToFilterView(param);
            return new SearchResult<ModelVehicleListModel, QueryParamModel>
            {
                Results = _mapper.Map<List<ModelVehicleListModel>>(await q.ToListAsync()),
                Param= param
            };
        }
        public async Task<ModelVehicleViewModel>AddModelAsync(ModelVehicleAddModel model)
        {
            var entity = _mapper.Map<Model>(model);
            entity.UpdatedAt = DateTime.Now;    
            await _db.Models.AddAsync(entity);
            await _db.SaveChangesAsync();
            return await FindAsync(entity.Id);
        }
        public async Task<ModelVehicleViewModel>EditModelAsync(ModelVehicleEditModel model)
        {
            var entity = await _db.Models.FirstOrDefaultAsync(x => x.Id == model.Id && x.Active);
            if(entity == null)
            {
                throw new Exception("Model not found");
            }
            _mapper.Map(model, entity);
            entity.UpdatedAt = DateTime.Now;
            await _db.SaveChangesAsync();
            return await FindAsync(entity.Id);
        }
        public async Task<ModelVehicleViewModel>RemoveModelAsync(RemoveModel model)
        {
            var entity = await _db.Models.FirstOrDefaultAsync(x => x.Id == model.Id && x.Active);
            if(entity == null)
            {
                throw new Exception("Model not found");
            }
            entity.Active = false;
            entity.UpdatedAt = DateTime.Now;
            await _db.SaveChangesAsync();
            return await FindAsync(entity.Id);
        }
    }
}
