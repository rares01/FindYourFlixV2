using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindYourFlix.Data.Entities;
using FindYourFlix.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FindYourFlix.Business.Users
{
    public class UserList
    {
        private readonly IRepository _repository;

        public UserList(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Model>> Select()
        {
            return await _repository.Query<User>()
                .Select(e => new Model
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    UserName = e.UserName,
                    Email = e.Email
                })
                .ToListAsync();
        }

        public class Model
        {
            public string Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
        }
    }
}