using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LetterBoxdContext.Migrations
{
    /// <inheritdoc />
    public partial class replacedTwoMoreImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 16,
                column: "ImageUrl",
                value: "https://cdn11.bigcommerce.com/s-yzgoj/images/stencil/1280x1280/products/2873628/5940935/MOVEB48955__98081.1679587033.jpg?c=2");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 20,
                column: "ImageUrl",
                value: "https://m.media-amazon.com/images/I/91AJYESZaVL._AC_UF894,1000_QL80_.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 16,
                column: "ImageUrl",
                value: "https://www.artymag.ir/media/zgalleries/05c0c73e79108a0bd4bef06e191b6584/parasite28.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 20,
                column: "ImageUrl",
                value: "https://sinarium.com/wp-content/uploads/2022/04/15-6.jpg");
        }
    }
}
