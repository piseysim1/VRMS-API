using Microsoft.EntityFrameworkCore;
using vrms_api.Extensions;
using vrms_api.Models;
using static vrms_api.Domains.VrmsDomain;
using static vrms_api.Models.ShareModel;

namespace vrms_api.Logics
{
    public class PaymentMethodLogic(IServiceProvider provider) :ShareLogic( provider)
    {
        public async Task<PaymentMethodViewModel>FindAsync(long id)
        {
            var enitiy = await _db.PaymentMethods.FirstOrDefaultAsync(x =>x.Id ==id && x.Active);
            if(enitiy == null)
            {
                throw new Exception("Payment Method not found");
            }
            return _mapper.Map<PaymentMethodViewModel>(enitiy);
        }
        public async Task<SearchResult<PaymentMethodListModel , QueryParamModel>> SearchAsync(QueryParamModel param)
        {
            var q = _db.PaymentMethods.Where(x => x.Active);
            var search = param.GetSearchFilter().ToLower();
            if (!string.IsNullOrEmpty(search)) 
            {
                q = q.Where(x => x.Name.ToLower().Contains(search));
            }
            await param.UpdateCountAsync(q);
            q = q.ToFilterView(param);
            return new SearchResult<PaymentMethodListModel, QueryParamModel>
            {
                Results = _mapper.Map<List<PaymentMethodListModel>>(await q.ToListAsync()),
                Param= param
            };
        }
        public async Task<PaymentMethodViewModel>AddPaymentMethodAsync(PaymentMethodAddModel model)     
        {
            var entity = _mapper.Map<PaymentMethod>(model);
            entity.UpdatedAt = DateTime.Now;    
            await _db.PaymentMethods.AddAsync(entity);
            await _db.SaveChangesAsync();
            return await FindAsync(entity.Id);
        }
        public async Task<PaymentMethodViewModel>EditPaymentMethodAsync(PaymentMethodEditModel model)
        {
            var entity = await _db.PaymentMethods.FirstOrDefaultAsync(x => x.Id == model.Id && x.Active);
            if (entity == null)
            {
                throw new Exception("Payment Method not found");
            }
            _mapper.Map(model, entity);
            entity.UpdatedAt = DateTime.Now;
            await _db.SaveChangesAsync();
            return await FindAsync(entity.Id);
        }
        public async Task<PaymentMethodViewModel> RemovePaymentMethodAsync(RemoveModel model)
        {
            var entity = await _db.PaymentMethods.FirstOrDefaultAsync(x => x.Id == model.Id && x.Active);
            if (entity == null)
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
