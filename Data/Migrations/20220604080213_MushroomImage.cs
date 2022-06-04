using Microsoft.EntityFrameworkCore.Migrations;

namespace Atlas.Data.Migrations
{
    public partial class MushroomImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Mushrooms",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Mushrooms");
        }
    }
}
