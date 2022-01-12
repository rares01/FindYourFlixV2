using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using FindYourFlix.Data.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace FindYourFlix.Data
{
    public class UserInfo: IUserInfo
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserInfo(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> GetUserId()
        {
            if (_httpContextAccessor.HttpContext == null)
                return null;
            
            var token = await _httpContextAccessor.HttpContext.GetTokenAsync("Bearer", "access_token");
            var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            return jwtSecurityToken.Claims.First(claim => claim.Type == "sid").Value;
        }
    }
}