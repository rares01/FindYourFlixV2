using System;
using System.Threading.Tasks;
using FindYourFlix.Data.Entities;
using FindYourFlix.Data.Interfaces;

namespace FindYourFlix.Business.Tags
{
    public class TagInsert
    {
        private readonly IRepository _repository;

        public TagInsert(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> Insert(Model model)
        {
            var tag = new Tag
            {
                Id = Guid.NewGuid().ToString(),
                MovieId = model.MovieId,
                UserId = model.UserId,
                Name = model.Name,
                InsertedDate = DateTime.UtcNow
            };
            
            await _repository.InsertAsync(tag);

            await _repository.SaveAsync();

            return tag.Id;
        }

        public class Model
        {
            public string MovieId { get; set; }
            public string UserId { get; set; }
            public string Name { get; set; }
        }
    }
}