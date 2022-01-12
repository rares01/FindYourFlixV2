using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace FindYourFlix.Data.Entities
{
    public class User 
    {
        public string Id { get; set; }
        public int Index { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime InsertedDate { get; set; }
        public List<LikedMovie> LikedMovies { get; set; }
        public List<Tag> Tags { get; set; }
    }
}