using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FindYourFlix.Business.Movies;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace FindYourFlix.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowAll")]
    public class MovieController : Controller
    {
        private readonly IServiceProvider _container;

        public MovieController(IServiceProvider container)
        {
            _container = container;
        }

        [HttpGet("{id}")]
        public async Task<MovieSelect.Model> Get(string id)
        {
            return await _container.GetService<MovieSelect>().Select(id);
        }
        
        [HttpGet]
        public async Task<IEnumerable<MovieList.Model>> Get()
        {
            return await _container.GetService<MovieList>().Select();
        }

        [HttpPost]
        public async Task Insert(MovieInsert.Model model)
        {
            await _container.GetService<MovieInsert>().Insert(model);
        }
    }
}