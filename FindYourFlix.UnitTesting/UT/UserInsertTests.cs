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

namespace FindYourFlix.UnitTesting.UT
{
    public class UserInsertTests
    {
        private readonly UserInsert _userInsert;
        private readonly IRepository _repository;
        private readonly Fixture _fixture;

        public UserInsertTests()
        {
            _fixture = new Fixture();
            _repository = A.Fake<IRepository>();
            _userInsert = new UserInsert(_repository);
        }

        [Fact]
        public async Task Can_insert_user()
        {
            var model = _fixture.Create<UserInsert.Model>();
            await _userInsert.Insert(model);
            A.CallTo(() => _repository.InsertAsync(A<User>.Ignored)).MustHaveHappenedOnceExactly();
        }

    }
}
