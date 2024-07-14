using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUniform.Migrations
{
    public partial class RelatedItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
