using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class n4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "Post",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Post",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Post_AdminId",
                table: "Post",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_UserId",
                table: "Post",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Admin_AdminId",
                table: "Post",
                column: "AdminId",
                principalTable: "Admin",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_User_UserId",
                table: "Post",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Admin_AdminId",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_User_UserId",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_AdminId",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_UserId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Post");
        }
    }
}
