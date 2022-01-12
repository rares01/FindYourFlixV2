using System;
using System.Linq;
using System.Threading.Tasks;
using FindYourFlix.Data.Entities;
using FindYourFlix.Data.Interfaces;
using FindYourFlix.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FindYourFlix.Business.Users
{
    public class UserLikeAction
    {
        private readonly IRepository _repository;
        private readonly IUserInfo _userInfo;

        public UserLikeAction(IRepository repository, IUserInfo userInfo)
        {
            _repository = repository;
            _userInfo = userInfo;
        }
        
        public async Task Insert(string movieId)
        {
            var userId = await _userInfo.GetUserId();
            
            if (await _repository.Query<User>()
                .Where(e => e.Id == userId)
                .FirstOrDefaultAsync() == null)
            {
                throw new ObjectNotFoundException($"User with id {userId} does not exist!");
            }
            
            if (await _repository.Query<Movie>()
                .Where(e => e.Id == movieId)
                .FirstOrDefaultAsync() == null)
            {
                throw new ObjectNotFoundException($"Movie with id {movieId} does not exist!");
            }

            if (await _repository.Query<LikedMovie>().AnyAsync(e => e.MovieId == movieId && e.UserId == userId))
            {
                var likedMovie = await _repository.Query<LikedMovie>()
                    .Where(e => e.MovieId == movieId && e.UserId == userId)
                    .FirstOrDefaultAsync();
                
                _repository.Delete<LikedMovie>(likedMovie.Id);
            }
            else
            {
                await _repository.InsertAsync(new LikedMovie
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = userId,
                    MovieId = movieId
                });
            }

            await _repository.SaveAsync();
        }

        public class Model
        {
            public string UserId { get; set; }
            public string MovieId { get; set; }
        }
    }
}