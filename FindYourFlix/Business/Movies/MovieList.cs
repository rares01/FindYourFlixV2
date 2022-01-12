using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindYourFlix.Data.Entities;
using FindYourFlix.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FindYourFlix.Business.Movies
{
    public class MovieList
    {
        private readonly IRepository _repository;
        private readonly IUserInfo _userInfo;

        public MovieList(IRepository repository, IUserInfo userInfo)
        {
            _repository = repository;
            _userInfo = userInfo;
        }

        public async Task<IEnumerable<Model>> Select()
        {
            var userId = await _userInfo.GetUserId();
            
            var movies = await _repository.Query<Movie>()
                .Select(e => new Model()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Genres = e.Genres.Select(g => g.Name).ToList(),
                    IsLiked = e.LikedMovies.Any(m => m.MovieId == e.Id && m.UserId == userId),
                    Tags = e.Tags.Select(x => new TagModel
                    {
                        Name = x.Name,
                        Id = x.Id,
                        IsUsersTag = x.UserId == userId
                    }).ToList()
                }).ToListAsync();
            
            return movies;
        }

        public class Model
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public bool IsLiked { get; set; }
            public List<string> Genres { get; set; }
            public List<TagModel> Tags { get; set; }
        }

        public class TagModel
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public bool IsUsersTag { get; set; }
        }
    }
}