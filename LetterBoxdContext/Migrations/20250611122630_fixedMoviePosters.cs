using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LetterBoxdContext.Migrations
{
    /// <inheritdoc />
    public partial class fixedMoviePosters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImageUrl",
                value: "https://m.media-amazon.com/images/I/71i-lmidyGL._AC_SL1500_.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 20,
                column: "ImageUrl",
                value: "https://m.media-amazon.com/images/I/91AJYESZaVL._AC_SL1500_.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImageUrl",
                value: "https://image.tmdb.org/t/p/original/rAiYTfKGqDCRIIqo664sY9XZIvQ.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 20,
                column: "ImageUrl",
                value: "https://image.tmdb.org/t/p/original/euDPyqLnuwaWMHajcU3oZ9uZezR.jpg");
        }
    }
}
