using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warsztat.Migrations
{
    public partial class addedOneRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "carId",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "clientId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_clientId",
                table: "Cars",
                column: "clientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Clients_clientId",
                table: "Cars",
                column: "clientId",
                principalTable: "Clients",
                principalColumn: "clientId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Clients_clientId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_clientId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "carId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "clientId",
                table: "Cars");
        }
    }
}
