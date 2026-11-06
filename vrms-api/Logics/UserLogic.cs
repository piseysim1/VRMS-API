using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Services;
using vrms_api.Extensions;
using vrms_api.Models;
using static vrms_api.Domains.VrmsDomain;
using static vrms_api.Models.ShareModel;

namespace vrms_api.Logics
{
    public class UserLogic(IServiceProvider provider) : ShareLogic(provider)
    {
        public async Task<SearchResult<UserListModel, QueryParamModel>> SearchAsync(QueryParamModel param)
        {
            var qUser = _db.Users.Where(x => x.Active);

            var q = from u in qUser
                    join ur in _db.UserRoles.Where(x => x.Active) on u.Id equals ur.UserId into urGroup
                    from ur in urGroup.DefaultIfEmpty()
                    join r in _db.Roles.Where(x => x.Active) on ur.RoleId equals r.Id into rGroup
                    from r in rGroup.DefaultIfEmpty()
                    select new UserListModel
                    {
                        Id = u.Id,
                        Username = u.Username,
                        FullName = u.FullName,
                        Phone = u.Phone,
                        Profile = u.Profile,
                        Email = u.Email,
                        RoleId = ur != null ? ur.RoleId : 0,
                        RoleName = r != null ? r.Name : string.Empty
                    };

           
            var searchFilter = param.GetSearchFilter()?.ToLower();

            if (!string.IsNullOrEmpty(searchFilter))
            {
                q = q.Where(x => (x.Username != null && x.Username.ToLower().Contains(searchFilter)) ||
                                 (x.FullName != null && x.FullName.ToLower().Contains(searchFilter)))
                                ;
            }
            await param.UpdateCountAsync(q);

          
            return new SearchResult<UserListModel, QueryParamModel>
            {
                Results = await q.ToFilterView(param).ToListAsync(),
                Param = param
            };
        }
        public async Task<UserViewModel> FindAsync(long id)
        {
            var entity = await _db.Users.FirstOrDefaultAsync(x => x.Id == id && x.Active);
            if (entity == null)
            {
                throw new Exception("Vehicle not found");
            }
            return _mapper.Map<UserViewModel>(entity);
        }

        public async Task<UserViewModel>AddAsync(UserAddModel model)
        {
            var existingUser = await _db.Users.FirstOrDefaultAsync(x => x.Username == model.Username && x.Active);
            if (existingUser != null)
            {
                throw new Exception("Username already exists");
            }
            var role = await _db.Roles.FirstOrDefaultAsync(r => r.Id == model.RoleId && r.Active);
            if (role == null)
            {
                throw new Exception("Selected Role does not exist or is inactive");
            }
            using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var entity = _mapper.Map<User>(model);
                entity.PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(model.Password);
                entity.CreatedAt = DateTime.Now;
                await _db.Users.AddAsync(entity);
                await _db.SaveChangesAsync();
                var userRole = new UserRole
                {
                    UserId = entity.Id,
                    RoleId = model.RoleId, 
                    CreatedAt = DateTime.Now
                };
                await _db.UserRoles.AddAsync(userRole);
                await _db.SaveChangesAsync();

                await transaction.CommitAsync();

                return await FindAsync(entity.Id);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public  async Task<UserViewModel>EditAsync(UserEditModel model)
        {
            var entity = await _db.Users.FirstOrDefaultAsync(x => x.Id == model.Id && x.Active);
            if (entity == null)
            {
                throw new Exception("User not found");
            }
            entity.UpdatedAt = DateTime.Now;
            _mapper.Map(model, entity);
            await _db.SaveChangesAsync();
            return await FindAsync(entity.Id);
        }

        public async Task<UserViewModel> RemoveAsync(RemoveModel model)
        {
            var entity = await _db.Users.FirstOrDefaultAsync(x => x.Id == model.Id && x.Active);
            if (entity == null)
            {
                throw new Exception("User not found");
            }
            entity.UpdatedAt = DateTime.Now;
            entity.Active = false;
            await _db.SaveChangesAsync();

            return await FindAsync(entity.Id);
        }
    }
}
