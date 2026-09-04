using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Services;
using System.Linq.Dynamic.Core;
using vrms_api.Extensions;
using vrms_api.Models;
using static vrms_api.Domains.VrmsDomain;
using static vrms_api.Models.ShareModel;

namespace vrms_api.Logics
{
    public class BrandLogic(IServiceProvider provider) : ShareLogic(provider)
    {
        public async Task<SearchResult<BrandListModel , QueryParamModel>> SearchAsync(QueryParamModel param)
        {
            var q = _db.Brands.Where(x => x.Active);
            var search = param.GetSearchFilter().ToLower();
            if (!string.IsNullOrEmpty(search)) 
            {
                q = q.Where(x => x.Name.ToLower().Contains(search));

            }
            await param.UpdateCountAsync(q);
            q = q.ToFilterView(param);
            return new SearchResult<BrandListModel, QueryParamModel>
            {
                Results = _mapper.Map<List<BrandListModel>>(await q.ToListAsync()),
                Param= param

            };

        }

        public async Task<BrandViewModel>FindAsync(long id)
        {
            var entity = await _db.Brands.FirstOrDefaultAsync(x => x.Id == id && x.Active);
            if (entity == null)
            {
                throw new Exception("Brand not found");
            }
            return _mapper.Map<BrandViewModel>(entity);
        }
        public async Task<BrandViewModel> BrandAddAsync(BrandAddModel model)
        {
            var entity = _mapper.Map<Brand>(model);
            entity.CreatedAt = DateTime.Now;
            await _db.Brands.AddAsync(entity);
            await _db.SaveChangesAsync();
            return await FindAsync(entity.Id);
        }
        public async Task<BrandViewModel> BrandEditAsync(BrandEditModel model)
        {
            var entity = await _db.Brands.FirstOrDefaultAsync(x => x.Id == model.Id && x.Active);
            if (entity == null)
            {
                throw new Exception("Brand not found");
            }
            _mapper.Map(model, entity);
            entity.UpdatedAt = DateTime.Now;
            await _db.SaveChangesAsync();
            return await FindAsync(entity.Id);
        }
        public async Task<BrandViewModel> BrandRemoveAsync(RemoveModel model)
        {
            var entity = await _db.Brands.FirstOrDefaultAsync(x => x.Id == model.Id && x.Active);
            if (entity == null)
            {
                throw new Exception("Brand not found");
            }
            model.Id = entity.Id;
            model.Note = entity.Note;
            entity.Active = false;
            entity.UpdatedAt = DateTime.Now;
            await _db.SaveChangesAsync();
            return _mapper.Map<BrandViewModel>(entity);
        }
        
    }
}
