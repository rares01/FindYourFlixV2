using System;
using System.Linq;
using System.Threading.Tasks;
using FindYourFlix.Data.Entities;
using FindYourFlix.Data.Interfaces;
using FindYourFlix.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FindYourFlix.Business.Tags
{
    public class TagDelete
    {
        private readonly IRepository _repository;

        public TagDelete(IRepository repository)
        {
            _repository = repository;
        }
        
        public async Task Delete(string id)
        {
            if (await _repository.Query<Tag>()
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync() == null)
            {
                throw new ObjectNotFoundException($"User with id {id} does not exist!");
            }
            
            _repository.Delete<Tag>(id);
            await _repository.SaveAsync();
        }
    }
}