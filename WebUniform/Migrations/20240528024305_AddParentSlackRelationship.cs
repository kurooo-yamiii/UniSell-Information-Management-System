using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUniform.Migrations
{
    public partial class AddParentSlackRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slacks_Slacks_SlackId",
                table: "Slacks");

            migrationBuilder.DropIndex(
                name: "IX_Slacks_SlackId",
                table: "Slacks");

            migrationBuilder.DropColumn(
                name: "SlackId",
                table: "Slacks");

            migrationBuilder.AddColumn<int>(
                name: "ParentSlackId",
                table: "Slacks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Slacks_ParentSlackId",
                table: "Slacks",
                column: "ParentSlackId");

            migrationBuilder.AddForeignKey(
                name: "FK_Slacks_Slacks_ParentSlackId",
                table: "Slacks",
                column: "ParentSlackId",
                principalTable: "Slacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slacks_Slacks_ParentSlackId",
                table: "Slacks");

            migrationBuilder.DropIndex(
                name: "IX_Slacks_ParentSlackId",
                table: "Slacks");

            migrationBuilder.DropColumn(
                name: "ParentSlackId",
                table: "Slacks");

            migrationBuilder.AddColumn<int>(
                name: "SlackId",
                table: "Slacks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Slacks_SlackId",
                table: "Slacks",
                column: "SlackId");

            migrationBuilder.AddForeignKey(
                name: "FK_Slacks_Slacks_SlackId",
                table: "Slacks",
                column: "SlackId",
                principalTable: "Slacks",
                principalColumn: "Id");
        }
    }
}
