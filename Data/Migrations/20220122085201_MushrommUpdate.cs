using Microsoft.EntityFrameworkCore.Migrations;

namespace Atlas.Data.Migrations
{
    public partial class MushrommUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CommonName",
                table: "Mushrooms",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommonName",
                table: "Mushrooms");
        }
    }
}
