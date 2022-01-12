using System;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using FindYourFlix.Data.Entities;
using FindYourFlix.Data.Interfaces;
using FindYourFlix.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FindYourFlix.Business.Users
{
    public class UserUpdate
    {
        private readonly IRepository _repository;
        private readonly IUserInfo _userInfo;

        public UserUpdate(IRepository repository, IUserInfo userInfo)
        {
            _repository = repository;
            _userInfo = userInfo;
        }
        

        public async Task Update(Model model)
        {
            var id = await _userInfo.GetUserId();
            if (await _repository.GetByIdAsync<User>(id) == null)
            {
                throw new ObjectNotFoundException($"User with id {id} does not exist!");
            }

            var user = await _repository.GetByIdAsync<User>(id);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.UserName;
            user.Email = model.Email;

            _repository.Update(user);
            await _repository.SaveAsync();
        }

        public class Model
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
        }
    }
}