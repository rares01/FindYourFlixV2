using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindYourFlix.Data.Entities;
using FindYourFlix.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FindYourFlix.Business.Tags
{
    public class TagList
    {
        private readonly IRepository _repository;

        public TagList(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Model>> Select()
        {
            return await _repository.Query<Tag>()
                .Select(e => new Model()
                {
                    Id = e.Id,
                    Name = e.Name,
                    UserName = $"{e.User.FirstName} {e.User.LastName}",
                    Movie = e.Movie.Name,
                    InsertedDate = DateTime.UtcNow
                }).ToListAsync();
        }

        public class Model
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string UserName { get; set; }
            public string Movie { get; set; }
            public DateTime InsertedDate { get; set; }
        }
    }
}