using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LetterBoxdContext.Migrations
{
    /// <inheritdoc />
    public partial class addedMoreMovies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "ImageUrl", "ReleaseYear", "Title" },
                values: new object[,]
                {
                    { 4, "The aging patriarch of an organized crime dynasty transfers control to his reluctant son.", "https://image.tmdb.org/t/p/original/3bhkrj58Vtu7enYsRolD1fZdja1.jpg", 1972, "The Godfather" },
                    { 5, "A thief who steals corporate secrets through dream-sharing is given the inverse task of planting an idea.", "https://image.tmdb.org/t/p/original/edv5CZvWj09upOsy2Y6IwDhK8bt.jpg", 2010, "Inception" },
                    { 6, "Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.", "https://image.tmdb.org/t/p/original/qJ2tW6WMUDux911r6m7haRef0WH.jpg", 2008, "The Dark Knight" },
                    { 7, "The lives of two mob hitmen, a boxer, and others intertwine in tales of violence and redemption.", "https://image.tmdb.org/t/p/original/d5iIlFn5s0ImszYzBPb8JPIfbXD.jpg", 1994, "Pulp Fiction" },
                    { 8, "A computer hacker learns the true nature of reality and his role in the war against its controllers.", "https://image.tmdb.org/t/p/original/f89U3ADr1oiB1s9GkdPOEpXUk5H.jpg", 1999, "The Matrix" },
                    { 9, "A team of explorers travel through a wormhole in space in an attempt to ensure humanity's survival.", "https://image.tmdb.org/t/p/original/rAiYTfKGqDCRIIqo664sY9XZIvQ.jpg", 2014, "Interstellar" },
                    { 10, "An insomniac office worker and a soapmaker form an underground fight club that evolves into something more.", "https://image.tmdb.org/t/p/original/bptfVGEQuv6vDTIMVCHjJ9Dz8PX.jpg", 1999, "Fight Club" },
                    { 11, "Forrest Gump, a man with a low IQ, recounts the early years of his life when he found himself in the middle of key historical events.", "https://image.tmdb.org/t/p/original/arw2vcBveWOVZr6pxd9XTd1TdQa.jpg", 1994, "Forrest Gump" },
                    { 12, "Two imprisoned men bond over years, finding solace and redemption through acts of decency.", "https://image.tmdb.org/t/p/original/q6y0Go1tsGEsmtFryDOJo3dEmqu.jpg", 1994, "The Shawshank Redemption" },
                    { 13, "A former Roman General sets out to exact vengeance against the corrupt emperor who murdered his family.", "https://image.tmdb.org/t/p/original/ty8TGRuvJLPUmAR1H1nRIsgwvim.jpg", 2000, "Gladiator" },
                    { 14, "Lion prince Simba and his father are targeted by his bitter uncle, who wants to ascend the throne himself.", "https://image.tmdb.org/t/p/original/sKCr78MXSLixwmZ8DyJLrpMsd15.jpg", 1994, "The Lion King" },
                    { 15, "Aspiring musician Miguel enters the Land of the Dead to find his great-great-grandfather, a legendary singer.", "https://image.tmdb.org/t/p/original/gGEsBPAijhVUFoiNpgZXqRVWJt2.jpg", 2017, "Coco" },
                    { 16, "Greed and class discrimination threaten the symbiotic relationship between a wealthy family and a poor one.", "https://image.tmdb.org/t/p/original/7IiTTgloJzvGI1TAYymCfbfl3vT.jpg", 2019, "Parasite" },
                    { 17, "After the devastating events of Infinity War, the Avengers assemble once more to undo Thanos's actions.", "https://image.tmdb.org/t/p/original/or06FN3Dka5tukK1e9sl16pB3iy.jpg", 2019, "Avengers: Endgame" },
                    { 18, "A young FBI cadet must confide in an incarcerated and manipulative killer to receive his help on catching another serial killer.", "https://image.tmdb.org/t/p/original/uS9m8OBk1A8eM9I042bx8XXpqAq.jpg", 1991, "The Silence of the Lambs" },
                    { 19, "The lives of guards on Death Row are affected by one of their charges: a black man accused of child murder and rape, yet who has a mysterious gift.", "https://image.tmdb.org/t/p/original/sOHqdY1RnSn6kcfAHKu28jvTebE.jpg", 1999, "The Green Mile" },
                    { 20, "An insurance salesman discovers his whole life is actually a reality TV show broadcast to the world.", "https://image.tmdb.org/t/p/original/euDPyqLnuwaWMHajcU3oZ9uZezR.jpg", 1998, "The Truman Show" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 20);
        }
    }
}
