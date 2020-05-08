using Microsoft.EntityFrameworkCore.Migrations;

namespace RedeSocial.Data.RedeSocial.Migrations
{
    public partial class AlteracaoUsuarioModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioModel",
                table: "UsuarioModel");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UsuarioModel");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityUser",
                table: "UsuarioModel",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioModel",
                table: "UsuarioModel",
                column: "IdentityUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioModel",
                table: "UsuarioModel");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityUser",
                table: "UsuarioModel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UsuarioModel",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioModel",
                table: "UsuarioModel",
                column: "Id");
        }
    }
}
