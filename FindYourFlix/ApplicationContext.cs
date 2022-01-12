using FindYourFlix.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FindYourFlix
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<LikedMovie> LikedMovie { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=FindYourFlix;Trusted_Connection=True;");
        }
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var likedMovies = modelBuilder.Entity<LikedMovie>();
            likedMovies.ToTable("LikedMovies");
            likedMovies.HasIndex(e => e.Id);
            likedMovies.Property(e => e.Id).HasMaxLength(36);
            likedMovies.HasOne(e => e.User)
                .WithMany(e => e.LikedMovies)
                .HasForeignKey(e => e.UserId); 
            likedMovies.HasOne(e => e.Movie)
                .WithMany(e => e.LikedMovies)
                .HasForeignKey(e => e.MovieId); 
            
            var users = modelBuilder.Entity<User>();
            users.ToTable("Users");
            users.HasIndex(e => e.Id);
            users.Property(e => e.Id).HasMaxLength(36);
            users.Property(e => e.FirstName).HasMaxLength(36);
            users.Property(e => e.LastName).HasMaxLength(36);
            users.Property(e => e.Email).HasMaxLength(100);
            users.Property(e => e.UserName).HasMaxLength(36);
            users.Property(e => e.InsertedDate).HasDefaultValueSql("getutcdate()");
            users.Property(e => e.Password);
            users.Property(e => e.Index);
            users.Property(e => e.IsAdmin).HasDefaultValue(false);
            
            var movies = modelBuilder.Entity<Movie>();
            movies.ToTable("Movies");
            movies.HasIndex(e => e.Id);
            movies.Property(e => e.Id).HasMaxLength(36);
            movies.Property(e => e.Name).HasMaxLength(100);
            movies.Property(e => e.Index);

            var genres = modelBuilder.Entity<Genre>();
            genres.ToTable("Genres");
            genres.HasIndex(e => e.Id);
            genres.Property(e => e.Id).HasMaxLength(36);
            genres.Property(e => e.Name).HasMaxLength(20);
            genres.HasOne(e => e.Movie)
                .WithMany(e => e.Genres)
                .HasForeignKey(e => e.MovieId);

            var tags = modelBuilder.Entity<Tag>();
            tags.ToTable("Tags");
            tags.HasIndex(e => e.Id);
            tags.Property(e => e.Id).HasMaxLength(36);
            tags.Property(e => e.Name).HasMaxLength(100);
            tags.Property(e => e.InsertedDate).HasDefaultValueSql("getutcdate()");
            tags.HasOne(e => e.User)
                .WithMany(e => e.Tags)
                .HasForeignKey(e => e.UserId);
            tags.HasOne(e => e.Movie)
                .WithMany(e => e.Tags)
                .HasForeignKey(e => e.MovieId);

        }
    }
}