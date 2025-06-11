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
                    Description = "A lone juror in a New York City murder trial refuses to convict without discussing the evidence, challenging the others to reexamine their assumptions and biases.",
                    ImageUrl = "https://media-cache.cinematerial.com/p/500x/c5whwpvz/12-angry-men-theatrical-movie-poster.jpg?v=1456708298"
                },
                new Movie { Id = 2,
                    Title = "Soul",
                    ReleaseYear = 2020,
                    Description = "A middle-school band teacher passionate about jazz journeys to a mysterious realm where he discovers the true meaning of life and soul.",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BZTZkYjA5MDEtMjY1ZC00ODk5LThjOTUtZDYxODEzYWNjMTU2XkEyXkFqcGc@._V1_.jpg"
                },
                new Movie {
                    Id = 3,
                    Title = "Avatar",
                    ReleaseYear = 2009,
                    Description = "A paraplegic Marine dispatched to the moon Pandora on a unique mission becomes torn between following his orders and protecting the world he feels is his home.",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BMTc3MDcwMTc1MV5BMl5BanBnXkFtZTcwMzk4NTU3Mg@@._V1_.jpg"
                },
                new Movie
                {
                    Id = 4,
                    Title = "The Godfather",
                    ReleaseYear = 1972,
                    Description = "The aging patriarch of an organized crime dynasty transfers control to his reluctant son.",
                    ImageUrl = "https://malltina.com/mag/wp-content/uploads/2019/03/The-Godfather-1082x1536.jpg"
                },
                new Movie
                {
                    Id = 5,
                    Title = "Inception",
                    ReleaseYear = 2010,
                    Description = "A thief who steals corporate secrets through dream-sharing is given the inverse task of planting an idea.",
                    ImageUrl = "https://malltina.com/mag/wp-content/uploads/2019/03/Inception_poster-500x740.jpg"
                },
                new Movie
                {
                    Id = 6,
                    Title = "The Dark Knight",
                    ReleaseYear = 2008,
                    Description = "Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BMTMxNTMwODM0NF5BMl5BanBnXkFtZTcwODAyMTk2Mw@@._V1_FMjpg_UX1000_.jpg"
                },
                new Movie
                {
                    Id = 7,
                    Title = "Pulp Fiction",
                    ReleaseYear = 1994,
                    Description = "The lives of two mob hitmen, a boxer, and others intertwine in tales of violence and redemption.",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BYTViYTE3ZGQtNDBlMC00ZTAyLTkyODMtZGRiZDg0MjA2YThkXkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg"
                },
                new Movie
                {
                    Id = 8,
                    Title = "The Matrix",
                    ReleaseYear = 1999,
                    Description = "A computer hacker learns the true nature of reality and his role in the war against its controllers.",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BN2NmN2VhMTQtMDNiOS00NDlhLTliMjgtODE2ZTY0ODQyNDRhXkEyXkFqcGc@._V1_.jpg"
                },
                new Movie
                {
                    Id = 9,
                    Title = "Interstellar",
                    ReleaseYear = 2014,
                    Description = "A team of explorers travel through a wormhole in space in an attempt to ensure humanity's survival.",
                    ImageUrl = "https://malltina.com/mag/wp-content/uploads/2019/03/32-500x714.jpeg"
                },
                new Movie
                {
                    Id = 10,
                    Title = "Fight Club",
                    ReleaseYear = 1999,
                    Description = "An insomniac office worker and a soapmaker form an underground fight club that evolves into something more.",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BOTgyOGQ1NDItNGU3Ny00MjU3LTg2YWEtNmEyYjBiMjI1Y2M5XkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg"
                },
                new Movie
                {
                    Id = 11,
                    Title = "Forrest Gump",
                    ReleaseYear = 1994,
                    Description = "Forrest Gump, a man with a low IQ, recounts the early years of his life when he found himself in the middle of key historical events.",
                    ImageUrl = "https://malltina.com/mag/wp-content/uploads/2019/03/71CHZi4vhWL._SL1500_-500x750.jpg"
                },
                new Movie
                {
                    Id = 12,
                    Title = "The Shawshank Redemption",
                    ReleaseYear = 1994,
                    Description = "Two imprisoned men bond over years, finding solace and redemption through acts of decency.",
                    ImageUrl = "https://malltina.com/mag/wp-content/uploads/2019/03/the-shawshank-redemption.jpg"
                },
                new Movie
                {
                    Id = 13,
                    Title = "Gladiator",
                    ReleaseYear = 2000,
                    Description = "A former Roman General sets out to exact vengeance against the corrupt emperor who murdered his family.",
                    ImageUrl = "https://malltina.com/mag/wp-content/uploads/2019/03/48-500x741.jpg"
                },
                new Movie
                {
                    Id = 14,
                    Title = "The Lion King",
                    ReleaseYear = 1994,
                    Description = "Lion prince Simba and his father are targeted by his bitter uncle, who wants to ascend the throne himself.",
                    ImageUrl = "https://malltina.com/mag/wp-content/uploads/2019/03/45-500x743.jpg"
                },
                new Movie
                {
                    Id = 15,
                    Title = "Coco",
                    ReleaseYear = 2017,
                    Description = "Aspiring musician Miguel enters the Land of the Dead to find his great-great-grandfather, a legendary singer.",
                    ImageUrl = "https://api2.zoomg.ir/media/2017-9-f6134586-1165-48cd-82fd-66ddbc25d776-66cc7cff2b5676090d006b91?w=1920&q=80"
                },
                new Movie
                {
                    Id = 16,
                    Title = "Parasite",
                    ReleaseYear = 2019,
                    Description = "Greed and class discrimination threaten the symbiotic relationship between a wealthy family and a poor one.",
                    ImageUrl = "https://m.media-amazon.com/images/I/91KArYP03YL._AC_UF894,1000_QL80_.jpg"
                },
                new Movie
                {
                    Id = 17,
                    Title = "Avengers: Endgame",
                    ReleaseYear = 2019,
                    Description = "After the devastating events of Infinity War, the Avengers assemble once more to undo Thanos's actions.",
                    ImageUrl = "https://api2.zoomg.ir/media/2019-7-3170707f-e771-4f73-a721-92061b25df87-66cc7d052b5676090d015469?w=1920&q=80"
                },
                new Movie
                {
                    Id = 18,
                    Title = "The Silence of the Lambs",
                    ReleaseYear = 1991,
                    Description = "A young FBI cadet must confide in an incarcerated and manipulative killer to receive his help on catching another serial killer.",
                    ImageUrl = "https://malltina.com/mag/wp-content/uploads/2019/03/24-1-500x738.jpg"
                },
                new Movie
                {
                    Id = 19,
                    Title = "The Green Mile",
                    ReleaseYear = 1999,
                    Description = "The lives of guards on Death Row are affected by one of their charges: a black man accused of child murder and rape, yet who has a mysterious gift.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/fa/thumb/c/ce/Green_mile.jpg/250px-Green_mile.jpg"
                },
                new Movie
                {
                    Id = 20,
                    Title = "The Truman Show",
                    ReleaseYear = 1998,
                    Description = "An insurance salesman discovers his whole life is actually a reality TV show broadcast to the world.",
                    ImageUrl = "https://m.media-amazon.com/images/I/91AJYESZaVL._AC_UF894,1000_QL80_.jpg"
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
