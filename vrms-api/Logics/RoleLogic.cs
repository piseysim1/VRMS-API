using Microsoft.EntityFrameworkCore;
using vrms_api.Extensions;
using vrms_api.Helper;
using vrms_api.Models;
using static vrms_api.Domains.VrmsDomain;
using static vrms_api.Models.ShareModel;

namespace vrms_api.Logics
{
    public class RoleLogic(IServiceProvider provider) : ShareLogic(provider)
    {
        public async Task<SearchResult<RoleListModel, QueryParamModel>> SearchAsync(QueryParamModel param)
        {
            var qRole = _db.Roles.Where(x => x.Active);
            if (string.IsNullOrEmpty(param.Sorts))
            {
                param.Sorts = nameof(Role.Name);
            }
            string searchFilter = param.GetSearchFilter()?.ToLower();
            if (!string.IsNullOrEmpty(searchFilter))
            {
                qRole = qRole.Where(x => x.Name.ToLower().Contains(searchFilter));
            }

            await param.UpdateCountAsync(qRole);
            qRole = qRole.ToFilterView(param);

            return new SearchResult<RoleListModel, QueryParamModel>
            {
                Results = _mapper.Map<List<RoleListModel>>(await qRole.ToListAsync()),
                Param = param,
            };
        }

        public async Task<ApiResponse<RoleViewModel>> AddAsync(RoleAddModel model)
        {
            var entry = _mapper.Map<Role>(model);
            entry.CreatedAt = DateTime.Now;
            await _db.Roles.AddAsync(entry);
            await _db.SaveChangesAsync();

            return await FindAsync(entry.Id);
        }
        public async Task<ApiResponse<RoleViewModel>> EditAsync(RoleEditModel model)
        {
            var entity = await _db.Roles.FirstOrDefaultAsync(x => x.Active && x.Id == model.Id);
            if (entity == null) 
            { 
                throw new Exception("Role Not Found");
            }

            _mapper.Map(entity, model);
            await _db.SaveChangesAsync();

            return await FindAsync(entity.Id);
        }
        public async Task<ApiResponse<RoleViewModel>> FindAsync(long id)
        {
            var entry = await _db.Roles.FirstOrDefaultAsync(x => x.Id == id);
            if (entry == null)
            {
                throw new Exception($"Found {id}");
            }

            return _mapper.Map<RoleViewModel>(entry).ToApiResponse();
        }
        public async Task<ApiResponse<RoleViewModel>> RemoveAsync(RemoveModel model)
        {
            var entity = await _db.Roles.FirstOrDefaultAsync(x => x.Active && x.Id == model.Id);
            if (entity == null)
            {
                throw new Exception("Role Not Found");
            }
            entity.Active = false;
            entity.UpdatedAt = DateTime.Now;
            await _db.SaveChangesAsync();
            return await FindAsync(entity.Id);
        }
    }
}
