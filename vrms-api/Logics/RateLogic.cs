using Microsoft.EntityFrameworkCore;
using vrms_api.Domains;
using vrms_api.Extensions;
using vrms_api.Models;
using static vrms_api.Domains.VrmsDomain;
using static vrms_api.Models.ShareModel;

namespace vrms_api.Logics
{
    public class RateLogic(IServiceProvider provider):ShareLogic(provider)
    {
        public async Task<SearchResult<RateListModel, QueryParamModel>> SearchAsync(QueryParamModel param)
        {
            var qRate = _db.Rates.Where(x => x.Active);
            var search = param.GetSearchFilter().ToLower();
            if (!string.IsNullOrEmpty(search))
            {
                qRate = qRate.Where(x => x.Code.ToLower().Contains(search));

            }

            var q = from r in qRate
                    join m in _db.Models.Where(x => x.Active) on r.ModelId equals m.Id
                    join lt in _db.LookUpItems.Where(x => x.Active && x.LookUpTypeId == (long)LookupTypes.RateType)
                    on r.RateTypeId equals lt.Id
                    select new RateListModel
                    {
                        Id = r.Id,
                        Code = r.Code,
                        ModelId = r.ModelId,
                        RateTypeId = r.RateTypeId,
                        ModelName = m.Name,
                        RateTypeName = lt.Name,
                        RateTypeImage = lt.ImageUrl
                    };

            await param.UpdateCountAsync(q);
            q = q.ToFilterView(param);
            return new SearchResult<RateListModel, QueryParamModel>
            {
                Results = _mapper.Map<List<RateListModel>>(await q.ToListAsync()),
                Param = param

            };

        }
        public async Task<RateViewModel> FindAsync(long id)
        {
            var entity = await _db.Rates.FirstOrDefaultAsync(x => x.Id == id && x.Active);
            if(entity == null)
            {
                throw new Exception("Rate not found");
            }
            return _mapper.Map<RateViewModel>(entity);
        }
        public async Task<RateViewModel> AddAsync(RateAddModel model)
        {
            var entity = _mapper.Map<Rate>(model);
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
            return  await FindAsync(entity.Id);
        }
        public async Task<RateViewModel> EditAsync(RateEditModel model)
        {
            var entity = await _db.Rates.FirstOrDefaultAsync(x => x.Id == model.Id && x.Active);
            if(entity == null)
            {
                throw new Exception("Rate not found");
            }
            _mapper.Map(model, entity);
            await _db.SaveChangesAsync();
            return await FindAsync(entity.Id);
        }
        public async Task<RateViewModel> RemoveAsync(RemoveModel model)
        {
            var entity = await _db.Rates.FirstOrDefaultAsync(x => x.Id == model.Id && x.Active);
            if(entity == null)
            {
                throw new Exception("Rate not found");
            }
            entity.Active = false;
            await _db.SaveChangesAsync();
            return await FindAsync(entity.Id);
        }
        
    }
}
