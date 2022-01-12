using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace FindYourFlix.Business
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly HttpContext _context;
        

        public JwtMiddleware(HttpContext context)
        {
            _context = context;
        }

        public async Task Invoke()
        {
            var token = _context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            await _next(_context);
        }
    }
}