using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warsztat.Migrations
{
    public partial class AddedRelationBetweenCarAndCarType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "carId",
                table: "CarTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "carTypeMark",
                table: "Cars",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_carTypeMark",
                table: "Cars",
                column: "carTypeMark");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarTypes_carTypeMark",
                table: "Cars",
                column: "carTypeMark",
                principalTable: "CarTypes",
                principalColumn: "mark",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarTypes_carTypeMark",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_carTypeMark",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "carId",
                table: "CarTypes");

            migrationBuilder.DropColumn(
                name: "carTypeMark",
                table: "Cars");
        }
    }
}
