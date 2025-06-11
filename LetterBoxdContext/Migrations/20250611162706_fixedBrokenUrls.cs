using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LetterBoxdContext.Migrations
{
    /// <inheritdoc />
    public partial class fixedBrokenUrls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 16,
                column: "ImageUrl",
                value: "https://m.media-amazon.com/images/I/91KArYP03YL._AC_UF894,1000_QL80_.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 16,
                column: "ImageUrl",
                value: "https://cdn11.bigcommerce.com/s-yzgoj/images/stencil/1280x1280/products/2873628/5940935/MOVEB48955__98081.1679587033.jpg?c=2");
        }
    }
}
