using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace style_catalog.Migrations.Account
{
    public partial class ColumnCleanup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Account");
        }
  }
}
