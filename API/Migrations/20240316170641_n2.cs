using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class n2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PwdHash",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PwdSalt",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PwdHash",
                table: "Admin",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PwdSalt",
                table: "Admin",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PwdHash",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PwdSalt",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PwdHash",
                table: "Admin");

            migrationBuilder.DropColumn(
                name: "PwdSalt",
                table: "Admin");
        }
    }
}
