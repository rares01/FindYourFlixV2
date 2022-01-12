using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using FindYourFlix.Data.Entities;
using FindYourFlix.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FindYourFlix.Business.Users
{
    public class UserUpdatePassword
    {
        private readonly IRepository _repository;
        private readonly IUserInfo _userInfo;

        public UserUpdatePassword(IRepository repository, IUserInfo userInfo)
        {
            _repository = repository;
            _userInfo = userInfo;
        }

        public async Task<bool> UpdatePassword(Model model)
        {
            var id = await _userInfo.GetUserId();
            var user = await _repository.Query<User>().Where(e => e.Id == id).FirstOrDefaultAsync();
            if (CheckOldPassword(model.OldPassword, user.Password))
            {
                user.Password = HashPassword(model.NewPassword);
                _repository.Update(user);
                await _repository.SaveAsync();
                return true;
            }

            return false;
        }

        private static bool CheckOldPassword(string insertedPassword, string password)
        {
            var hashBytes = Convert.FromBase64String(password);
            var salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(insertedPassword, salt, 100000);
            var hash = pbkdf2.GetBytes(20);
            for (var i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    return false;
            return true;
        }
        
        private static string HashPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            var hash = pbkdf2.GetBytes(20);
            var  hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return Convert.ToBase64String(hashBytes);
        }

        public class Model
        {
            public string OldPassword { get; set; }
            public string NewPassword { get; set; }
        }
    }
}