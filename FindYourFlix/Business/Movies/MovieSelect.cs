using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindYourFlix.Data.Entities;
using FindYourFlix.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FindYourFlix.Business.Movies
{
    public class MovieSelect
    {
        private readonly IRepository _repository;

        public MovieSelect(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Model> Select(string id)
        {
            var movie = await  _repository.Query<Movie>()
                .Where(e => e.Id == id)
                .Select(e => new Model
                {
                    Name = e.Name,
                    Genres = e.Genres.Select(g => g.Name).ToList()
                })
                .FirstOrDefaultAsync();

            return movie;
        }

        public class Model
        {
            public string Name { get; set; }
            public List<string> Genres { get; set; }
        }
    }
}