using System;
using System.Threading.Tasks;
using Xunit;

using FindYourFlix.Data;
using FindYourFlix.Data.Entities;
using FakeItEasy;
using FindYourFlix.Business.Users;
using FindYourFlix.Data.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using AutoFixture;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using FindYourFlix.Controllers;

namespace FindYourFlix.UT
{
    public class FirstTestRunning
    {
        private readonly UserController _controller;
        private readonly IServiceProvider _container;
        private readonly Fixture _fixture;
        public FirstTestRunning()
        {
            _fixture = new Fixture();
            _container = A.Fake<IServiceProvider>();
            _controller = new UserController(_container);
        }

        [Fact]
        public async Task Can_Insert()
        {
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
    .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var model = _fixture.Create<UserInsert.Model>();
            var user = _fixture.Create<User>();
            var options = _fixture.Create<DbContextOptions<ApplicationContext>>();
            var appContext = A.Fake<ApplicationContext>(x => x.WithArgumentsForConstructor(() => new ApplicationContext(options)));
            var repository = A.Fake<Repository>(x => x.WithArgumentsForConstructor(() => new Repository(appContext)));
            await _controller.Insert(model);
            A.CallTo(() => repository.InsertAsync(user)).MustHaveHappenedOnceExactly();
        }
    }
}

