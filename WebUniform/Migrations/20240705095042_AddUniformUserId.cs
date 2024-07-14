using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUniform.Migrations
{
    public partial class AddUniformUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slacks_Slacks_ParentSlackId",
                table: "Slacks");

            migrationBuilder.RenameColumn(
                name: "ParentSlackId",
                table: "Slacks",
                newName: "SlackId");

            migrationBuilder.RenameIndex(
                name: "IX_Slacks_ParentSlackId",
                table: "Slacks",
                newName: "IX_Slacks_SlackId");

            migrationBuilder.AddColumn<int>(
                name: "UniformId",
                table: "Uniforms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Uniforms_UniformId",
                table: "Uniforms",
                column: "UniformId");

            migrationBuilder.AddForeignKey(
                name: "FK_Slacks_Slacks_SlackId",
                table: "Slacks",
                column: "SlackId",
                principalTable: "Slacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Uniforms_Uniforms_UniformId",
                table: "Uniforms",
                column: "UniformId",
                principalTable: "Uniforms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slacks_Slacks_SlackId",
                table: "Slacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Uniforms_Uniforms_UniformId",
                table: "Uniforms");

            migrationBuilder.DropIndex(
                name: "IX_Uniforms_UniformId",
                table: "Uniforms");

            migrationBuilder.DropColumn(
                name: "UniformId",
                table: "Uniforms");

            migrationBuilder.RenameColumn(
                name: "SlackId",
                table: "Slacks",
                newName: "ParentSlackId");

            migrationBuilder.RenameIndex(
                name: "IX_Slacks_SlackId",
                table: "Slacks",
                newName: "IX_Slacks_ParentSlackId");

            migrationBuilder.AddForeignKey(
                name: "FK_Slacks_Slacks_ParentSlackId",
                table: "Slacks",
                column: "ParentSlackId",
                principalTable: "Slacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
