using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using vrms_api.Domains;
using vrms_api.Helper;
using vrms_api.Models;
using static vrms_api.Domains.VrmsDomain;
using static vrms_api.Models.ShareModel;

namespace vrms_api.Logics
{
    public class AuthLogic(IServiceProvider provider):ShareLogic(provider)
    {
        public async Task<ApiResponse<string>> Register(RegisterModel model)
        {
            var existingUser = await _db.Users.FirstOrDefaultAsync(x => x.Username == model.Username && x.Active);
            if (existingUser != null)
            {
                throw new Exception("User already exists");
            }
            var defaultRoleId = (long)RoleEnum.User;
            var role = await _db.Roles.FirstOrDefaultAsync(r => r.Id == defaultRoleId && r.Active);
            if (role == null)
            {
                throw new Exception("Default 'User' role not found in system");
            }

            using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                
                var user = _mapper.Map<User>(model);
                user.PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(model.Password);
                user.Active = true;
                user.CreatedAt = DateTime.Now;

                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync(); 

                
                var userRole = new UserRole
                {
                    UserId = user.Id,
                    RoleId = defaultRoleId,
                    Active = true,
                    CreatedAt = DateTime.Now
                };

                await _db.UserRoles.AddAsync(userRole);
                await _db.SaveChangesAsync();

                await transaction.CommitAsync();

                return "Registered successfully".ToApiResponse();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
             
        }
        public async Task<ApiResponse<AuthResponseModel>> Login(LoginModel model)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == model.Username && u.Active);
            if (user == null || !BCrypt.Net.BCrypt.EnhancedVerify(model.Password, user.PasswordHash))
            {
               throw new Exception("Invalid username or password");
            }
            var roles = await (from ur in _db.UserRoles.Where(x => x.Active)
                               join r in _db.Roles.Where(x => x.Active) on ur.RoleId equals r.Id
                               where ur.UserId == user.Id
                               select r.Id.ToString()).ToListAsync();

            var accessToken = GenerateJwtToken(user, roles);
            var refreshToken = GenerateRefreshToken();

            var session = new LoginSession
            {
                UserId = user.Id,
                Token = refreshToken,
                ExpiresAt = DateTime.Now.AddDays(Utl.Zvl(_config["JwtConfig:RefreshTokenExpiration"], 7)),
                IsRevoked = false
            };
            _db.LoginSessions.Add(session);
            var res = new AuthResponseModel { AccessToken = accessToken, RefreshToken = refreshToken };

            return res.ToApiResponse();
        }

       
        public async Task<ApiResponse<AuthResponseModel>> Refresh(RefreshTokenModel model)
        {
            var session = await _db.LoginSessions
                                   .FirstOrDefaultAsync(s => s.Token == model.RefreshToken && !s.IsRevoked);

            if (session == null || session.ExpiresAt < DateTime.Now)
            {
                throw new Exception("Invalid refresh token");
            }

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == session.UserId);
            if (user == null) {
                throw new Exception("User not Found");
            
            };

            var permissions = await (from ur in _db.UserRoles.Where(x => x.Active)
                                     join r in _db.Roles.Where(x => x.Active) on ur.RoleId equals r.Id
                                     where ur.UserId == user.Id
                                     select r.Id.ToString()).ToListAsync();

            var newAccessToken = GenerateJwtToken(user, permissions);

            return new AuthResponseModel { AccessToken = newAccessToken, RefreshToken = model.RefreshToken }.ToApiResponse();
        }
        public async Task<ApiResponse<string>> Logout(RefreshTokenModel model)
        {
            var session = await _db.LoginSessions.FirstOrDefaultAsync(s => s.Token == model.RefreshToken);

            if (session != null)
            {
                session.LogOutDate = DateTime.Now;
                session.IsRevoked = true;
                await _db.SaveChangesAsync();
            }

            return "Logged out successfully".ToApiResponse();
        }
        private string GenerateJwtToken(User user, List<string> roles)
        {
            var claims = new List<Claim> {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.UniqueName, user.Username),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new("uid", user.Id.ToString())
            };

            roles.ForEach(r => claims.Add(new Claim(ClaimTypes.Role, r)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtConfig:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _config["JwtConfig:Issuer"],
                audience: _config["JwtConfig:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Utl.Zvl(_config["JwtConfig:AccessTokenExpiration"], 60)),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }

    }
}
