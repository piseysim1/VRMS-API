using Microsoft.EntityFrameworkCore;
using vrms_api.Domains;
using vrms_api.Extensions;
using vrms_api.Models;
using static vrms_api.Domains.VrmsDomain;
using static vrms_api.Models.ShareModel;

namespace vrms_api.Logics
{
    public class BookingLogic (IServiceProvider provider):ShareLogic(provider)
    {
        public async Task<SearchResult<BookingListModel, QueryParamModel>> SearchAsync(QueryParamModel param)
        {
            var qBooking = _db.Bookings.Where(x => x.Active);
            var search = param.GetSearchFilter()?.ToLower();

            var q = from b in qBooking
                     
                    join u in _db.Users.Where(x => x.Active) on b.UserId equals u.Id
                    join bs in _db.LookUpItems.Where(x => x.Active && x.LookUpTypeId == (long)LookUpTypes.BusinessSource)
                        on b.BusinessSourceId equals bs.Id into bsGroup
                    from bs in bsGroup.DefaultIfEmpty()
                    join st in _db.LookUpItems on b.StatusId equals st.Id into stGroup
                    from st in stGroup.DefaultIfEmpty()

                    select new BookingListModel
                    {
                        Id = b.Id,
                        BookingNo = b.BookingNo,
                        UserId = b.UserId,
                        Username = u.FullName,
                        BusinessSourceId = b.BusinessSourceId,
                        BusinessSourceName = bs != null ? bs.Name : string.Empty, 
                        StartDate = b.StartDate,
                        EndDate = b.EndDate,
                        StatusId = b.StatusId,
                        StatusName = st != null ? st.NameEn : string.Empty,
                        Total = b.Total,
                        IdentityCardUrl = b.IdentityCardUrl,
                        VehicleDocumentUrl = b.VehicleDocumentUrl,
                        PaymentProofUrl = b.PaymentProofUrl,
                        VehicleRegistrationUrl = b.VehicleRegistrationUrl,
                        LocationId = b.LocationId,
                        PaymentMethodId = b.PaymentMethodId

                    };

            if (!string.IsNullOrEmpty(search))
            {
                q = q.Where(x => x.BookingNo.ToLower().Contains(search) ||
                                 x.Username.ToLower().Contains(search) ||
                                 x.BusinessSourceName.ToLower().Contains(search));
            }
            await param.UpdateCountAsync(q);
            return new SearchResult<BookingListModel, QueryParamModel>
            {
                Results = await q.ToFilterView(param).ToListAsync(),
                Param = param
            };
        }
        public async Task<BookingViewModel> FindAsync(long id)
        {
            var entity = await _db.Bookings.FirstOrDefaultAsync(x => x.Id == id && x.Active);
            if (entity == null)
            {
                throw new Exception("Booking not found");
            }
            return _mapper.Map<BookingViewModel>(entity);

        }
        public async Task<BookingViewModel>BookingAddAsync(BookingAddModel model)
        {
            var entity = _mapper.Map<Booking>(model);
            var pending = await _db.LookUpItems.FirstAsync(x => x.Active
                && x.LookUpTypeId == (long)LookUpTypes.BookingStatus && x.NameEn == "Pending");
            entity.StatusId = pending.Id;
            entity.CreatedAt = DateTime.Now;
            await _db.Bookings.AddAsync(entity);
            await _db.SaveChangesAsync();
            return await FindAsync(entity.Id);
        }
        public async Task<BookingViewModel> BookingEditAsync(BookingEditModel model)
        {
            var entity = await _db.Bookings.FirstOrDefaultAsync(x => x.Id == model.Id && x.Active);
            if (entity == null)
            {
                throw new Exception("Booking not found");
            }
            _mapper.Map(model, entity);
            entity.UpdatedAt = DateTime.Now;
            await _db.SaveChangesAsync();
            return await FindAsync(entity.Id);
        }
        public async Task<BookingViewModel> BookingRemoveAsync(RemoveModel model)
        {
            var entity = await _db.Bookings.FirstOrDefaultAsync(x => x.Id == model.Id && x.Active);
            if (entity == null)
            {
                throw new Exception("Booking not found");
            }
            entity.Active = false;
            entity.UpdatedAt = DateTime.Now;
            await _db.SaveChangesAsync();
            return await FindAsync(entity.Id);
        }
        public async Task<BookingViewModel> ChangeStatusAsync(BookingStatusModel m)
        {
            var e = await _db.Bookings.FirstOrDefaultAsync(x => x.Id == m.Id && x.Active);
            if (e == null) throw new Exception("Booking not found");
            e.StatusId = m.StatusId;
            e.UpdatedAt = DateTime.Now;
            await _db.SaveChangesAsync();
            return await FindAsync(e.Id);
        }
    }
}
