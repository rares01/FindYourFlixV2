using System.Collections.Generic;

namespace FindYourFlix.Data.Entities
{
    public class LikedMovie
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string MovieId { get; set; }
        public User User { get; set; }
        public Movie Movie { get; set; }
    }
}