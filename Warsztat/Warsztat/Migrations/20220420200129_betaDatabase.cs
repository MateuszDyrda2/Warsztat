using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warsztat.Migrations
{
    public partial class betaDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityDictionaries",
                columns: table => new
                {
                    activityType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    activityName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityDictionaries", x => x.activityType);
                });

            migrationBuilder.CreateTable(
                name: "CarTypes",
                columns: table => new
                {
                    mark = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    model = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarTypes", x => x.mark);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    clientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    surrname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    carId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.clientId);
                });

            migrationBuilder.CreateTable(
                name: "Personels",
                columns: table => new
                {
                    personelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    surrname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    requestId = table.Column<int>(type: "int", nullable: false),
                    activityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personels", x => x.personelId);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    carId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    registrationNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    clientId = table.Column<int>(type: "int", nullable: false),
                    carTypeMark = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    requestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.carId);
                    table.ForeignKey(
                        name: "FK_Cars_CarTypes_carTypeMark",
                        column: x => x.carTypeMark,
                        principalTable: "CarTypes",
                        principalColumn: "mark",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cars_Clients_clientId",
                        column: x => x.clientId,
                        principalTable: "Clients",
                        principalColumn: "clientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    requestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "varchar(3)", nullable: false),
                    dateTimeOfRequestStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateTimeOfRequestEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    carId = table.Column<int>(type: "int", nullable: false),
                    personelId = table.Column<int>(type: "int", nullable: false),
                    activityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.requestId);
                    table.ForeignKey(
                        name: "FK_Requests_Cars_carId",
                        column: x => x.carId,
                        principalTable: "Cars",
                        principalColumn: "carId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requests_Personels_personelId",
                        column: x => x.personelId,
                        principalTable: "Personels",
                        principalColumn: "personelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    activityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sequenceNumber = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    dateTimeOfActivityStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateTimeOfActivityEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    requestId = table.Column<int>(type: "int", nullable: false),
                    personelId = table.Column<int>(type: "int", nullable: false),
                    activityType = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.activityId);
                    table.ForeignKey(
                        name: "FK_Activities_ActivityDictionaries_activityType",
                        column: x => x.activityType,
                        principalTable: "ActivityDictionaries",
                        principalColumn: "activityType",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Activities_Personels_personelId",
                        column: x => x.personelId,
                        principalTable: "Personels",
                        principalColumn: "personelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Activities_Requests_requestId",
                        column: x => x.requestId,
                        principalTable: "Requests",
                        principalColumn: "requestId",
                        onDelete: ReferentialAction.NoAction);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Cars_carTypeMark",
                table: "Cars",
                column: "carTypeMark");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_clientId",
                table: "Cars",
                column: "clientId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_carId",
                table: "Requests",
                column: "carId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_personelId",
                table: "Requests",
                column: "personelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "ActivityDictionaries");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Personels");

            migrationBuilder.DropTable(
                name: "CarTypes");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
