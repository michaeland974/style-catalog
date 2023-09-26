using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace style_catalog.Migrations.Account
{
    public partial class AccountModelCleanup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
              name: "id",
              table: "Account",
              newName: "Id");

            migrationBuilder.RenameColumn(
              name: "username",
              table: "Account",
              newName: "UserName");

            migrationBuilder.RenameColumn(
              name: "password",
              table: "Account",
              newName: "Password");

            migrationBuilder.RenameColumn(
              name: "confirmPassword",
              table: "Account",
              newName: "ConfirmPassword");
          
            migrationBuilder.DropColumn(
                  name: "firstName",
                  table: "Account");
        }
  }
}