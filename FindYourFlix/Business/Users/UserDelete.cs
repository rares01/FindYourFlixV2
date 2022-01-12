using System;
using System.Linq;
using System.Threading.Tasks;
using FindYourFlix.Data.Entities;
using FindYourFlix.Data.Interfaces;
using FindYourFlix.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FindYourFlix.Business.Users
{
    public class UserDelete
    {
        private readonly IRepository _repository;

        public UserDelete(IRepository repository)
        {
            _repository = repository;
        }

        public async Task Delete(string id)
        {
            if (await _repository.GetByIdAsync<User>(id) == null)
            {
                throw new ObjectNotFoundException($"User with id {id} does not exist!");
            }
            
            _repository.Delete<User>(id);
            await _repository.SaveAsync();
        }
    }
}