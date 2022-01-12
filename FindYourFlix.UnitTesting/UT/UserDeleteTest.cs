using AutoFixture;
using FakeItEasy;
using FindYourFlix.Business.Users;
using FindYourFlix.Data.Entities;
using FindYourFlix.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using FindYourFlix.Exceptions;

namespace FindYourFlix.UnitTesting.UT
{
    public class UserDeleteTest
    {
        private readonly UserDelete _userDelete;
        private readonly IRepository _repository;
        

        public UserDeleteTest()
        {
            
            _repository = A.Fake<IRepository>();
            _userDelete = new UserDelete(_repository);
        }

        [Fact]
        public async Task Can_delete_user()
        {
            User user = new User
            {
                Id = new Guid("046F3577-3E05-4B56-BB78-380176CA4635").ToString()
            };
            var userID = user.Id;
            A.CallTo(()=> _repository.GetByIdAsync<User>((new Guid("046F3577-3E05-4B56-BB78-380176CA4636")).ToString())).Returns(user);
            await _userDelete.Delete(userID);


            A.CallTo(() => _repository.SaveAsync()).MustHaveHappenedOnceExactly();
            
        }

        [Fact]
        public async Task Can_delete_user_null()
        {
            User user = new User
            {
                Id = new Guid("046F3577-3E05-4B56-BB78-380176CA4635").ToString()
            };
            var userID = user.Id;
            A.CallTo(() => _repository.GetByIdAsync<User>(A<string>.Ignored)).Returns((User)null);


            Assert.ThrowsAsync<ObjectNotFoundException>(async () => await _userDelete.Delete(userID));

           
        }
    }
}
