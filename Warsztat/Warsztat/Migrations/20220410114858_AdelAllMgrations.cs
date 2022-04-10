using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warsztat.Migrations
{
    public partial class AdelAllMgrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Requests",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "carId",
                table: "CarTypes");

            migrationBuilder.AlterColumn<int>(
                name: "carId",
                table: "Requests",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "requestId",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "activityId",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "personelId",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "activityId",
                table: "Personels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requestId",
                table: "Personels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requestId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "activityType",
                table: "Activities",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "personelId",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requestId",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Requests",
                table: "Requests",
                column: "requestId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_carId",
                table: "Requests",
                column: "carId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_personelId",
                table: "Requests",
                column: "personelId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_activityType",
                table: "Activities",
                column: "activityType");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_personelId",
                table: "Activities",
                column: "personelId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_requestId",
                table: "Activities",
                column: "requestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_ActivityDictionaries_activityType",
                table: "Activities",
                column: "activityType",
                principalTable: "ActivityDictionaries",
                principalColumn: "activityType",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Personels_personelId",
                table: "Activities",
                column: "personelId",
                principalTable: "Personels",
                principalColumn: "personelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Requests_requestId",
                table: "Activities",
                column: "requestId",
                principalTable: "Requests",
                principalColumn: "requestId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Cars_carId",
                table: "Requests",
                column: "carId",
                principalTable: "Cars",
                principalColumn: "carId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Personels_personelId",
                table: "Requests",
                column: "personelId",
                principalTable: "Personels",
                principalColumn: "personelId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_ActivityDictionaries_activityType",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Personels_personelId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Requests_requestId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Cars_carId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Personels_personelId",
                table: "Requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Requests",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_carId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_personelId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Activities_activityType",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_personelId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_requestId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "requestId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "activityId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "personelId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "activityId",
                table: "Personels");

            migrationBuilder.DropColumn(
                name: "requestId",
                table: "Personels");

            migrationBuilder.DropColumn(
                name: "requestId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "activityType",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "personelId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "requestId",
                table: "Activities");

            migrationBuilder.AlterColumn<int>(
                name: "carId",
                table: "Requests",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "carId",
                table: "CarTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Requests",
                table: "Requests",
                column: "carId");
        }
    }
}
