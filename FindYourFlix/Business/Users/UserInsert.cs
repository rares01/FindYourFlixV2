using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using FindYourFlix.Data.Entities;
using FindYourFlix.Data.Interfaces;

namespace FindYourFlix.Business.Users
{
    public class UserInsert
    {
        private readonly IRepository _repository;
        
        public UserInsert(IRepository repository)
        {
            _repository = repository;
        }

        public async Task Insert(Model model)
        {
            await _repository.InsertAsync(new User
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
                Password = HashPassword(model.Password),
                InsertedDate = DateTime.UtcNow
            });

            await _repository.SaveAsync();
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
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

    }
}