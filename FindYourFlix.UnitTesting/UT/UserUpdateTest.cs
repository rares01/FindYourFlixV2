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
using static FindYourFlix.Business.Users.UserUpdate;
using FindYourFlix.Exceptions;

namespace FindYourFlix.UnitTesting.UT
{
    public class UserUpdateTest
    {
        private readonly UserUpdate _userUpdate;
        private readonly IRepository _repository;
        private readonly IUserInfo _userInfo;
        private readonly Fixture _fixture;


        public UserUpdateTest()
        {
            _repository = A.Fake<IRepository>();
            _userInfo = A.Fake<IUserInfo>();
            _userUpdate = new UserUpdate(_repository, _userInfo);
            _fixture = new Fixture();

        }

        [Fact]
        public async Task Can_update_user()
        {
            var model = _fixture.Create<Model>();
            User user = new User
            {
                Id = new Guid("046F3577-3E05-4B56-BB78-380176CA4635").ToString()
            };
            var userID = user.Id;
            A.CallTo(() => _repository.GetByIdAsync<User>((new Guid("046F3577-3E05-4B56-BB78-380176CA4636")).ToString())).Returns(user);


            await _userUpdate.Update(model);


            A.CallTo(() => _repository.SaveAsync()).MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public async Task Can_update_user_null()
        {

            User user = new User
            {
                Id = new Guid("046F3577-3E05-4B56-BB78-380176CA4635").ToString()
            };
            var model = _fixture.Create<Model>();
            
            A.CallTo(() => _repository.GetByIdAsync<User>(A<string>.Ignored)).Returns((User)null);
            //await _userUpdate.Update(model);

            Assert.ThrowsAsync<ObjectNotFoundException>(async () => await _userUpdate.Update(model));


        }
    }
}
