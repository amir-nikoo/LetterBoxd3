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
                new Movie { Id = 1, Title = "Inception", ReleaseYear = 2010 },
                new Movie { Id = 2, Title = "Interstellar", ReleaseYear = 2014 },
                new Movie { Id = 3, Title = "Avatar", ReleaseYear = 2009 }
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
            .HasOne<User>()
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
