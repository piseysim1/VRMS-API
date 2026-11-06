using Microsoft.EntityFrameworkCore;
using vrms_api.Extensions;
using vrms_api.Models;
using static vrms_api.Domains.VrmsDomain;
using static vrms_api.Models.ShareModel;

namespace vrms_api.Logics
{
    public class BookingDetailLogic(IServiceProvider provider) : ShareLogic(provider)
    {
        public async Task<SearchResult<BookingDetailListModel, QueryParamModel>> SearchAsync(QueryParamModel param)
        {
            var qBookDetail = _db.BookingDetails.Where(x => x.Active);
            if (string.IsNullOrEmpty(param.Sorts))
            {
                param.Sorts = nameof(BookingDetail.BookingId);
            }
            string searchFilter = param.GetSearchFilter()?.ToLower();
            if (!string.IsNullOrEmpty(searchFilter))
            {
                qBookDetail = qBookDetail.Where(x => x.BookingId.ToString().ToLower().Contains(searchFilter));
            }
            await param.UpdateCountAsync(qBookDetail);
            qBookDetail = qBookDetail.ToFilterView(param);
            return new SearchResult<BookingDetailListModel, QueryParamModel>
            {
                Results = _mapper.Map<List<BookingDetailListModel>>(await qBookDetail.ToListAsync()),
                Param = param,
            };
        }
        public async Task<BookingDetailViewModel> FindAsync(long id)
        {
            var entity = await _db.BookingDetails.FirstOrDefaultAsync(x => x.Id == id && x.Active);
            if (entity == null)
            {
                throw new Exception("Booking Detail not found");
            }
            return _mapper.Map<BookingDetailViewModel>(entity);

        }
        public async Task<BookingDetailViewModel> AddAsync(BookingDetailAddModel model)
        {
            var entity = _mapper.Map<BookingDetail>(model);
            entity.CreatedAt = DateTime.Now;
            await _db.BookingDetails.AddAsync(entity);
            await _db.SaveChangesAsync();
            return await FindAsync(entity.Id);
        }
        public async Task<BookingDetailViewModel> EditAsync(BookingDetailEditModel model)
        {
            var entity = await _db.BookingDetails.FirstOrDefaultAsync(x => x.Id == model.Id && x.Active);
            if (entity == null)
            {
                throw new Exception("Booking Detail not found");
            }
            _mapper.Map(model, entity);
            entity.UpdatedAt = DateTime.Now;
            await _db.SaveChangesAsync();
            return await FindAsync(entity.Id);
        }
        public async Task<BookingDetailViewModel> RemoveAsync(RemoveModel model)
        {
            var entity = await _db.BookingDetails.FirstOrDefaultAsync(x => x.Id == model.Id && x.Active);
            if (entity == null)
            {
                throw new Exception("Booking Detail not found");
            }
            entity.Active = false;
            entity.UpdatedAt = DateTime.Now;
            await _db.SaveChangesAsync();
            return await FindAsync(entity.Id);
        }
    }
}
