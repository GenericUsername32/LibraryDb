using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryProject.Migrations
{
    public partial class reservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReserved",
                table: "Book",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "reserveUserId",
                table: "Book",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Book_reserveUserId",
                table: "Book",
                column: "reserveUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_AspNetUsers_reserveUserId",
                table: "Book",
                column: "reserveUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_AspNetUsers_reserveUserId",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_reserveUserId",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "IsReserved",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "reserveUserId",
                table: "Book");
        }
    }
}
