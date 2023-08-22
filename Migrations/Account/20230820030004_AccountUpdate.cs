using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace style_catalog.Migrations.Account
{
    /// <inheritdoc />
    public partial class AccountUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Account",
                newName: "confirmPassword");

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "Account",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "password",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Account");

            migrationBuilder.RenameColumn(
                name: "confirmPassword",
                table: "Account",
                newName: "PasswordHash");
        }
    }
}
