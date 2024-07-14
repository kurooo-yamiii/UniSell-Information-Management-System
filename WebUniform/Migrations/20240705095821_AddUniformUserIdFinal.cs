using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUniform.Migrations
{
    public partial class AddUniformUserIdFinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slacks_Slacks_SlackId",
                table: "Slacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Uniforms_Uniforms_UniformId",
                table: "Uniforms");

            migrationBuilder.AlterColumn<int>(
                name: "UniformId",
                table: "Uniforms",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SlackId",
                table: "Slacks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AlterColumn<int>(
                name: "UniformId",
                table: "Uniforms",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SlackId",
                table: "Slacks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
    }
}
