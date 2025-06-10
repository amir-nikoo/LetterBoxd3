using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LetterBoxdContext.Migrations
{
    /// <inheritdoc />
    public partial class fixDescriptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "A lone juror in a New York City murder trial refuses to convict without discussing the evidence, challenging the others to reexamine their assumptions and biases.");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "A middle-school band teacher passionate about jazz journeys to a mysterious realm where he discovers the true meaning of life and soul.");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "A paraplegic Marine dispatched to the moon Pandora on a unique mission becomes torn between following his orders and protecting the world he feels is his home.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "s.");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "l.");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "e.");
        }
    }
}
