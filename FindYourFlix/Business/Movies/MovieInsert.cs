using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindYourFlix.Data.Entities;
using FindYourFlix.Data.Interfaces;

namespace FindYourFlix.Business.Movies
{
    public class MovieInsert
    {
        private readonly IRepository _repository;
        
        public MovieInsert(IRepository repository)
        {
            _repository = repository;
        }

        public async Task Insert(Model model)
        {
            var guid = Guid.NewGuid();
            var movie = new Movie 
            {
                Id = guid.ToString(),
                Index = BitConverter.ToInt32(guid.ToByteArray(), 0),
                Name = model.Name
            };
            await _repository.InsertAsync(movie);

            foreach (var genre in model.Genres)
            {
                await _repository.InsertAsync(new Genre
                {
                    Id = Guid.NewGuid().ToString(),
                    MovieId = movie.Id,
                    Name = genre
                });
            }
            
            await _repository.SaveAsync();
        }

        public class Model
        {
            public string Name { get; set; }
            public List<string> Genres { get; set; }
        }

    }
}