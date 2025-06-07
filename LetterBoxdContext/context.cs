using LetterBoxdDomain;
using Microsoft.EntityFrameworkCore;

namespace LetterBoxdContext
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasData(
                new Movie { Id = 1,
                    Title = "12 Angry Men",
                    ReleaseYear = 1957,
                    Description = "The jury in a New York City murder trial is frustrated by a single member whose skeptical caution forces them to more carefully consider the evidence before jumping to a hasty verdict.",
                    ImageUrl = "https://media-cache.cinematerial.com/p/500x/c5whwpvz/12-angry-men-theatrical-movie-poster.jpg?v=1456708298"
                },
                new Movie { Id = 2,
                    Title = "Soul",
                    ReleaseYear = 2020,
                    Description = "Joe is a middle-school band teacher whose life hasn't quite gone the way he expected. His true passion is jazz. But when he travels to another realm to help someone find their passion, he soon discovers what it means to have soul.",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BZTZkYjA5MDEtMjY1ZC00ODk5LThjOTUtZDYxODEzYWNjMTU2XkEyXkFqcGc@._V1_.jpg"
                },
                new Movie {
                    Id = 3,
                    Title = "Avatar",
                    ReleaseYear = 2009,
                    Description = "A paraplegic Marine dispatched to the moon Pandora on a unique mission becomes torn between following his orders and protecting the world he feels is his home.",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BMTc3MDcwMTc1MV5BMl5BanBnXkFtZTcwMzk4NTU3Mg@@._V1_.jpg"
                }
            );

            modelBuilder.Entity<Comment>()
            .HasOne<Movie>()
            .WithMany(m => m.Comments)
            .HasForeignKey(c => c.MovieId)
            .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Rating>()
            .HasOne<Movie>()
            .WithMany(m => m.Ratings)
            .HasForeignKey(r => r.MovieId)
            .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.SetNull);


            modelBuilder.Entity<User>()
            .HasMany<Rating>()
            .WithOne()
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.SetNull);


            base.OnModelCreating(modelBuilder);
        }
    }
}
