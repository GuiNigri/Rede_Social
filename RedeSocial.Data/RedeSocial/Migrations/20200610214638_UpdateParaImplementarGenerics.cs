using Microsoft.EntityFrameworkCore.Migrations;

namespace RedeSocial.Data.RedeSocial.Migrations
{
    public partial class UpdateParaImplementarGenerics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioModel",
                table: "UsuarioModel");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityUser",
                table: "UsuarioModel",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UsuarioModel",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioModel",
                table: "UsuarioModel",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioModel",
                table: "UsuarioModel",
                column: "IdentityUser");
        }
    }
}
