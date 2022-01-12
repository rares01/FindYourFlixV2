using System.Linq;
using System.Threading.Tasks;
using FindYourFlix.Data.Entities;
using FindYourFlix.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FindYourFlix.Business.Users
{
    public class UserRoleUpdate
    {
        private readonly IRepository _repository;
        private readonly IUserInfo _userInfo;

        public UserRoleUpdate(IRepository repository, IUserInfo userInfo)
        {
            _repository = repository;
            _userInfo = userInfo;
        }
        

        public async Task Update(Model model)
        {
            var user = await _repository.Query<User>()
                .Where(e => e.Id == model.UserId).FirstOrDefaultAsync();
            user.IsAdmin = !user.IsAdmin;
            _repository.Update(user);
            await _repository.SaveAsync();
        }

        public class Model
        {
            public string UserId { get; set; }
        }
    }
}