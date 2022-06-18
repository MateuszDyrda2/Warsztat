using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warsztat.Migrations
{
    public partial class alphaDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Personels_personelId",
                table: "Activities");

            migrationBuilder.AlterColumn<int>(
                name: "personelId",
                table: "Activities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Personels_personelId",
                table: "Activities",
                column: "personelId",
                principalTable: "Personels",
                principalColumn: "personelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Personels_personelId",
                table: "Activities");

            migrationBuilder.AlterColumn<int>(
                name: "personelId",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Personels_personelId",
                table: "Activities",
                column: "personelId",
                principalTable: "Personels",
                principalColumn: "personelId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
