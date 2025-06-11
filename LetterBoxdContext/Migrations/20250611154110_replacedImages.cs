using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LetterBoxdContext.Migrations
{
    /// <inheritdoc />
    public partial class replacedImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "https://malltina.com/mag/wp-content/uploads/2019/03/The-Godfather-1082x1536.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "https://malltina.com/mag/wp-content/uploads/2019/03/Inception_poster-500x740.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "https://m.media-amazon.com/images/M/MV5BMTMxNTMwODM0NF5BMl5BanBnXkFtZTcwODAyMTk2Mw@@._V1_FMjpg_UX1000_.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageUrl",
                value: "https://m.media-amazon.com/images/M/MV5BYTViYTE3ZGQtNDBlMC00ZTAyLTkyODMtZGRiZDg0MjA2YThkXkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageUrl",
                value: "https://m.media-amazon.com/images/M/MV5BN2NmN2VhMTQtMDNiOS00NDlhLTliMjgtODE2ZTY0ODQyNDRhXkEyXkFqcGc@._V1_.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImageUrl",
                value: "https://malltina.com/mag/wp-content/uploads/2019/03/32-500x714.jpeg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImageUrl",
                value: "https://m.media-amazon.com/images/M/MV5BOTgyOGQ1NDItNGU3Ny00MjU3LTg2YWEtNmEyYjBiMjI1Y2M5XkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 11,
                column: "ImageUrl",
                value: "https://malltina.com/mag/wp-content/uploads/2019/03/71CHZi4vhWL._SL1500_-500x750.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 12,
                column: "ImageUrl",
                value: "https://malltina.com/mag/wp-content/uploads/2019/03/the-shawshank-redemption.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 13,
                column: "ImageUrl",
                value: "https://malltina.com/mag/wp-content/uploads/2019/03/48-500x741.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 14,
                column: "ImageUrl",
                value: "https://malltina.com/mag/wp-content/uploads/2019/03/45-500x743.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 15,
                column: "ImageUrl",
                value: "https://api2.zoomg.ir/media/2017-9-f6134586-1165-48cd-82fd-66ddbc25d776-66cc7cff2b5676090d006b91?w=1920&q=80");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 16,
                column: "ImageUrl",
                value: "https://www.artymag.ir/media/zgalleries/05c0c73e79108a0bd4bef06e191b6584/parasite28.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 17,
                column: "ImageUrl",
                value: "https://api2.zoomg.ir/media/2019-7-3170707f-e771-4f73-a721-92061b25df87-66cc7d052b5676090d015469?w=1920&q=80");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 18,
                column: "ImageUrl",
                value: "https://malltina.com/mag/wp-content/uploads/2019/03/24-1-500x738.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 19,
                column: "ImageUrl",
                value: "https://upload.wikimedia.org/wikipedia/fa/thumb/c/ce/Green_mile.jpg/250px-Green_mile.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 20,
                column: "ImageUrl",
                value: "https://sinarium.com/wp-content/uploads/2022/04/15-6.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "https://image.tmdb.org/t/p/original/3bhkrj58Vtu7enYsRolD1fZdja1.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "https://image.tmdb.org/t/p/original/edv5CZvWj09upOsy2Y6IwDhK8bt.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "https://image.tmdb.org/t/p/original/qJ2tW6WMUDux911r6m7haRef0WH.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageUrl",
                value: "https://image.tmdb.org/t/p/original/d5iIlFn5s0ImszYzBPb8JPIfbXD.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageUrl",
                value: "https://image.tmdb.org/t/p/original/f89U3ADr1oiB1s9GkdPOEpXUk5H.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImageUrl",
                value: "https://m.media-amazon.com/images/I/71i-lmidyGL._AC_SL1500_.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImageUrl",
                value: "https://image.tmdb.org/t/p/original/bptfVGEQuv6vDTIMVCHjJ9Dz8PX.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 11,
                column: "ImageUrl",
                value: "https://image.tmdb.org/t/p/original/arw2vcBveWOVZr6pxd9XTd1TdQa.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 12,
                column: "ImageUrl",
                value: "https://image.tmdb.org/t/p/original/q6y0Go1tsGEsmtFryDOJo3dEmqu.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 13,
                column: "ImageUrl",
                value: "https://image.tmdb.org/t/p/original/ty8TGRuvJLPUmAR1H1nRIsgwvim.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 14,
                column: "ImageUrl",
                value: "https://image.tmdb.org/t/p/original/sKCr78MXSLixwmZ8DyJLrpMsd15.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 15,
                column: "ImageUrl",
                value: "https://image.tmdb.org/t/p/original/gGEsBPAijhVUFoiNpgZXqRVWJt2.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 16,
                column: "ImageUrl",
                value: "https://image.tmdb.org/t/p/original/7IiTTgloJzvGI1TAYymCfbfl3vT.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 17,
                column: "ImageUrl",
                value: "https://image.tmdb.org/t/p/original/or06FN3Dka5tukK1e9sl16pB3iy.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 18,
                column: "ImageUrl",
                value: "https://image.tmdb.org/t/p/original/uS9m8OBk1A8eM9I042bx8XXpqAq.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 19,
                column: "ImageUrl",
                value: "https://image.tmdb.org/t/p/original/sOHqdY1RnSn6kcfAHKu28jvTebE.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 20,
                column: "ImageUrl",
                value: "https://m.media-amazon.com/images/I/91AJYESZaVL._AC_SL1500_.jpg");
        }
    }
}
