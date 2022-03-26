using Microsoft.EntityFrameworkCore.Migrations;

namespace Atlas.Data.Migrations
{
    public partial class PoleUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Mushrooms",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Mushrooms");
        }
    }
}
