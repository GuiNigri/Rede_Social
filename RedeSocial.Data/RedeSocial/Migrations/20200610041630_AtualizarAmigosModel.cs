using Microsoft.EntityFrameworkCore.Migrations;

namespace RedeSocial.Data.RedeSocial.Migrations
{
    public partial class AtualizarAmigosModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "AmigosModel");

            migrationBuilder.DropColumn(
                name: "UserId2",
                table: "AmigosModel");

            migrationBuilder.AddColumn<string>(
                name: "UserIdSolicitado",
                table: "AmigosModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserIdSolicitante",
                table: "AmigosModel",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserIdSolicitado",
                table: "AmigosModel");

            migrationBuilder.DropColumn(
                name: "UserIdSolicitante",
                table: "AmigosModel");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "AmigosModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId2",
                table: "AmigosModel",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
