using Microsoft.EntityFrameworkCore;
using vrms_api.Models;
using static vrms_api.Domains.SettingDomain;
using static vrms_api.Models.ShareModel;


namespace vrms_api.Logics
{
    public class LookUpTypeLogic(IServiceProvider provider):ShareLogic(provider)
    {
        public async Task<LookUpTypeViewModel> AddAsync(LookUpTypeAddModel model)
        {
            // map model to domain entity
            var entity = _mapper.Map<LookUpType>(model);
            entity.CreatedAt = DateTime.Now;
            await _db.LookUpTypes.AddAsync(entity);
            await _db.SaveChangesAsync();


            return await FindAsync(entity.Id);
        }

        public async Task<LookUpTypeViewModel> FindAsync(long id)
        {
            var entity = await _db.LookUpTypes.FirstOrDefaultAsync(x => x.Id == id && x.Active);
            if (entity == null)
            {
                throw new Exception("Leave type not found");
            }

            return _mapper.Map<LookUpTypeViewModel>(entity);
        }
        public async Task<LookUpTypeViewModel>EditAsync(LookUpTypeEditModel model)
        {
            var entity = await _db.LookUpTypes.FirstOrDefaultAsync(x => x.Active && x.Id == model.Id);
            if (entity == null) throw new Exception("Look up item not found");
            _mapper.Map(model, entity);
            await _db.SaveChangesAsync();

            return await FindAsync(entity.Id);
        }
        public async Task<LookUpTypeViewModel> RemoveModelAsync(RemoveModel model)
        {
            var entity = await _db.LookUpTypes.FirstOrDefaultAsync(x => x.Id == model.Id && x.Active);
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
