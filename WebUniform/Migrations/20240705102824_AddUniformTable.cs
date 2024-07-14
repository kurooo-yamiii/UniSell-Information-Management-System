using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUniform.Migrations
{
    public partial class AddUniformTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slacks_Slacks_ParentSlackId",
                table: "Slacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Uniforms_Uniforms_ParentUniformId",
                table: "Uniforms");

            migrationBuilder.DropIndex(
                name: "IX_Uniforms_ParentUniformId",
                table: "Uniforms");

            migrationBuilder.DropIndex(
                name: "IX_Slacks_ParentSlackId",
                table: "Slacks");

            migrationBuilder.DropColumn(
                name: "ParentUniformId",
                table: "Uniforms");

            migrationBuilder.DropColumn(
                name: "ParentSlackId",
                table: "Slacks");

            migrationBuilder.CreateIndex(
                name: "IX_Uniforms_UniformId",
                table: "Uniforms",
                column: "UniformId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Uniforms_Uniforms_UniformId",
                table: "Uniforms",
                column: "UniformId",
                principalTable: "Uniforms",
                principalColumn: "Id");
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

            migrationBuilder.DropIndex(
                name: "IX_Slacks_SlackId",
                table: "Slacks");

            migrationBuilder.AddColumn<int>(
                name: "ParentUniformId",
                table: "Uniforms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ParentSlackId",
                table: "Slacks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Uniforms_ParentUniformId",
                table: "Uniforms",
                column: "ParentUniformId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Uniforms_Uniforms_ParentUniformId",
                table: "Uniforms",
                column: "ParentUniformId",
                principalTable: "Uniforms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
