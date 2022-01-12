using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FindYourFlix.Business.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace FindYourFlix.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IServiceProvider _container;

        public UserController(IServiceProvider container)
        {
            _container = container;
        }

        [HttpGet]
        public async Task<UserSelect.Model> Get()
        {
            return await _container.GetService<UserSelect>().Select();
        }

        [HttpGet("list")]
        public async Task<IEnumerable<UserList.Model>> GetAll()
        {
            return await _container.GetService<UserList>().Select();
        }

        [HttpPut]
        public async Task Update(UserUpdate.Model model)
        {
            await _container.GetService<UserUpdate>().Update(model);
        }

        [HttpPut("update-password")]
        public async Task<bool> UpdatePassword(UserUpdatePassword.Model model)
        {
            return await _container.GetService<UserUpdatePassword>().UpdatePassword(model);
        }

        [HttpPost]
        public async Task Insert(UserInsert.Model model)
        {
            await _container.GetService<UserInsert>().Insert(model);
        }
        
        [HttpPost("{movieId}/like")]
        public async Task Insert(string movieId)
        {
            await _container.GetService<UserLikeAction>().Insert(movieId);
        }

        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _container.GetService<UserDelete>().Delete(id);
        }
    }
}