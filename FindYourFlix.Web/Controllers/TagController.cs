using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FindYourFlix.Business.Tags;
using FindYourFlix.Business.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace FindYourFlix.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagController : Controller
    {
        private readonly IServiceProvider _container;

        public TagController(IServiceProvider container)
        {
            _container = container;
        }
        
        [HttpGet]
        public async Task<IEnumerable<TagList.Model>> Select()
        {
            return await _container.GetService<TagList>().Select();
        }
        
        [HttpPost]
        public async Task<string> Insert(TagInsert.Model model)
        {
            return await _container.GetService<TagInsert>().Insert(model);
        }
        
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _container.GetService<TagDelete>().Delete(id);
        }
    }
}