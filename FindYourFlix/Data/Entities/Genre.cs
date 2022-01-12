namespace FindYourFlix.Data.Entities
{
    public class Genre
    {
        public string Id { get; set; }
        public string MovieId { get; set; }
        public string Name { get; set; }
        public Movie Movie { get; set; }
    }
}