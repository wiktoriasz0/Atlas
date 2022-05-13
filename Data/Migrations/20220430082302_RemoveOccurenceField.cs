using Microsoft.EntityFrameworkCore.Migrations;

namespace Atlas.Data.Migrations
{
    public partial class RemoveOccurenceField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Occurence",
                table: "Mushrooms");

            migrationBuilder.CreateIndex(
                name: "IX_MushroomInOccurences_MushroomID",
                table: "MushroomInOccurences",
                column: "MushroomID");

            migrationBuilder.CreateIndex(
                name: "IX_MushroomInOccurences_OccurenceID",
                table: "MushroomInOccurences",
                column: "OccurenceID");

            migrationBuilder.AddForeignKey(
                name: "FK_MushroomInOccurences_Mushrooms_MushroomID",
                table: "MushroomInOccurences",
                column: "MushroomID",
                principalTable: "Mushrooms",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MushroomInOccurences_Occurences_OccurenceID",
                table: "MushroomInOccurences",
                column: "OccurenceID",
                principalTable: "Occurences",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MushroomInOccurences_Mushrooms_MushroomID",
                table: "MushroomInOccurences");

            migrationBuilder.DropForeignKey(
                name: "FK_MushroomInOccurences_Occurences_OccurenceID",
                table: "MushroomInOccurences");

            migrationBuilder.DropIndex(
                name: "IX_MushroomInOccurences_MushroomID",
                table: "MushroomInOccurences");

            migrationBuilder.DropIndex(
                name: "IX_MushroomInOccurences_OccurenceID",
                table: "MushroomInOccurences");

            migrationBuilder.AddColumn<string>(
                name: "Occurence",
                table: "Mushrooms",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
