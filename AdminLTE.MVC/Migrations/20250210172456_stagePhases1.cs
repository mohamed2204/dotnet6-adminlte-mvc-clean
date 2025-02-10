using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminLTE.MVC.Migrations
{
    public partial class stagePhases1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Promotion",
                table: "Stages",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Stages",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Phases",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Promotion",
                table: "Stages",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Stages",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Phases",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
