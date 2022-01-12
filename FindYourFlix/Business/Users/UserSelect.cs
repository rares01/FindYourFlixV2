using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindYourFlix.Data.Entities;
using FindYourFlix.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FindYourFlix.Business.Users
{
    public class UserSelect
    {
        private readonly IRepository _repository;
        private readonly IUserInfo _userInfo;

        public UserSelect(IRepository repository, IUserInfo userInfo)
        {
            _repository = repository;
            _userInfo = userInfo;
        }
        
        public async Task<Model> Select()
        {
            var id = await _userInfo.GetUserId();
            var user = await _repository.Query<User>()
                .Where(e => e.Id == id)
                .Select(e => new Model
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    UserName = e.UserName,
                    Email = e.Email,
                    LikedMovies = e.LikedMovies.Select(m => m.Movie.Name).ToList()
                })
                .FirstOrDefaultAsync();
            return user;
        }

        public class Model
        {
            public string Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public List<string> LikedMovies { get; set; }
        }
        
    }
}