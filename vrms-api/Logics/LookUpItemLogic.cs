using Microsoft.EntityFrameworkCore;
using vrms_api.Models;
using static vrms_api.Domains.SettingDomain;
using static vrms_api.Models.ShareModel;

namespace vrms_api.Logics
{
    public class LookUpItemLogic(IServiceProvider provider): ShareLogic(provider)
    {
        public async Task<ViewResult<LookUpItemViewModel>> FindAsync(long id)
        {
            var entity = await (from li in _db.LookUpItems.Where(x => x.Id == id)
                                join lt in _db.LookUpTypes on li.LookUpTypeId equals lt.Id into types
                                from lt in types.DefaultIfEmpty() 
                                select new LookUpItemViewModel
                                {
                                    Id = li.Id,
                                    Name = li.Name,
                                    NameEn = li.NameEn,
                                    LookUpTypeId = li.LookUpTypeId,
                                    Color = li.Color,
                                    Note = li.Note!,
                                    ImageUrl = li.ImageUrl
                                }).FirstOrDefaultAsync();

            if (entity == null)
            {
                return new ViewResult<LookUpItemViewModel>
                {
                    Result = null,
                    Message = "Look up item not found",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            return new ViewResult<LookUpItemViewModel>
            {
                Result = entity,
                Message = null,
                StatusCode = StatusCodes.Status200OK
            };
        }
        public async Task<ViewResult<LookUpItemViewModel>> AddAsync(LookUpItemAddModel model)
        {
            var entry = _mapper.Map<LookUpItem>(model);
            entry.CreatedAt = DateTime.Now;
            await _db.LookUpItems.AddAsync(entry);
            await _db.SaveChangesAsync();

            return await FindAsync(entry.Id);
        }
        public async Task<ViewResult<LookUpItemViewModel>> EditAsync(LookUpItemEditModel model)
        {
            var entity = await _db.LookUpItems.FirstOrDefaultAsync(x => x.Active && x.Id == model.Id);
            if (entity == null) throw new Exception("Look up item not found");
            _mapper.Map(entity, model);
            await _db.SaveChangesAsync();

            return await FindAsync(entity.Id);
        }
        public async Task<ViewResult<LookUpItemViewModel>> RemoveModelAsync(RemoveModel model)
        {
            var entity = await _db.LookUpItems.FirstOrDefaultAsync(x => x.Id == model.Id && x.Active);
            if (entity == null)
            {
                throw new Exception("Look up item not found");
            }
            entity.Active = false;
            entity.UpdatedAt = DateTime.Now;
            await _db.SaveChangesAsync();
            return await FindAsync(entity.Id);
        }
    }
}
