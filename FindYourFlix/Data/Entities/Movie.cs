using System.Collections.Generic;

namespace FindYourFlix.Data.Entities
{
    public class Movie
    {
        public string Id { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public List<Genre> Genres { get; set; }
        public List<LikedMovie> LikedMovies { get; set; }
        public List<Tag> Tags { get; set; }
    }
}