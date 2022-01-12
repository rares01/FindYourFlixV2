using System;

namespace FindYourFlix.Data.Entities
{
    public class Tag
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string MovieId { get; set; }
        public string Name { get; set; }
        public DateTime InsertedDate { get; set; }
        public User User { get; set; }
        public Movie Movie { get; set; }
    }
}