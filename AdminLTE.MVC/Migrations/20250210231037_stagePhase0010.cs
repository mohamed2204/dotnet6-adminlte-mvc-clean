using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminLTE.MVC.Migrations
{
    public partial class stagePhase0010 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phases_Phases_PhaseId",
                table: "Phases");

            migrationBuilder.DropForeignKey(
                name: "FK_Phases_Stages_StageId",
                table: "Phases");

            migrationBuilder.DropIndex(
                name: "IX_Phases_PhaseId",
                table: "Phases");

            migrationBuilder.DropIndex(
                name: "IX_Phases_StageId",
                table: "Phases");

            migrationBuilder.DropColumn(
                name: "PhaseId",
                table: "Phases");

            migrationBuilder.DropColumn(
                name: "StageId",
                table: "Phases");

            migrationBuilder.CreateTable(
                name: "StagePhases",
                columns: table => new
                {
                    StageId = table.Column<int>(type: "INTEGER", nullable: false),
                    PhaseId = table.Column<int>(type: "INTEGER", nullable: false),
                    AddedOn = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StagePhases", x => new { x.StageId, x.PhaseId });
                    table.ForeignKey(
                        name: "FK_StagePhases_Phases_PhaseId",
                        column: x => x.PhaseId,
                        principalTable: "Phases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StagePhases_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StagePhases_PhaseId",
                table: "StagePhases",
                column: "PhaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StagePhases");

            migrationBuilder.AddColumn<int>(
                name: "PhaseId",
                table: "Phases",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StageId",
                table: "Phases",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Phases_PhaseId",
                table: "Phases",
                column: "PhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Phases_StageId",
                table: "Phases",
                column: "StageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Phases_Phases_PhaseId",
                table: "Phases",
                column: "PhaseId",
                principalTable: "Phases",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Phases_Stages_StageId",
                table: "Phases",
                column: "StageId",
                principalTable: "Stages",
                principalColumn: "Id");
        }
    }
}
