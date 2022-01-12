using AutoFixture;
using FakeItEasy;
using FindYourFlix.Business.Tags;
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
    public class TagInsertTest
    {

        private readonly TagInsert _tagInsert;
        private readonly IRepository _repository;
        private readonly Fixture _fixture;
        public TagInsertTest()
        {
            _fixture = new Fixture();
            _repository = A.Fake<IRepository>();
            _tagInsert = new TagInsert(_repository);
        }


        [Fact]
        public async Task Can_insert_tag()
        {
            var model = _fixture.Create<TagInsert.Model>();
            await _tagInsert.Insert(model);
            A.CallTo(() => _repository.InsertAsync(A<Tag>.Ignored)).MustHaveHappenedOnceExactly();
        }
    }
}
