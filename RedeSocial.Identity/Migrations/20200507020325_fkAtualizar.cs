using Microsoft.EntityFrameworkCore.Migrations;

namespace RedeSocial.Identity.Migrations
{
    public partial class fkAtualizar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosDB_AspNetUsers_PerfilId1",
                table: "UsuariosDB");

            migrationBuilder.DropIndex(
                name: "IX_UsuariosDB_PerfilId1",
                table: "UsuariosDB");

            migrationBuilder.DropColumn(
                name: "PerfilId",
                table: "UsuariosDB");

            migrationBuilder.DropColumn(
                name: "PerfilId1",
                table: "UsuariosDB");

            migrationBuilder.AddColumn<int>(
                name: "IdentityUserId",
                table: "UsuariosDB",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId1",
                table: "UsuariosDB",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosDB_IdentityUserId1",
                table: "UsuariosDB",
                column: "IdentityUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosDB_AspNetUsers_IdentityUserId1",
                table: "UsuariosDB",
                column: "IdentityUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosDB_AspNetUsers_IdentityUserId1",
                table: "UsuariosDB");

            migrationBuilder.DropIndex(
                name: "IX_UsuariosDB_IdentityUserId1",
                table: "UsuariosDB");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "UsuariosDB");

            migrationBuilder.DropColumn(
                name: "IdentityUserId1",
                table: "UsuariosDB");

            migrationBuilder.AddColumn<int>(
                name: "PerfilId",
                table: "UsuariosDB",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PerfilId1",
                table: "UsuariosDB",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosDB_PerfilId1",
                table: "UsuariosDB",
                column: "PerfilId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosDB_AspNetUsers_PerfilId1",
                table: "UsuariosDB",
                column: "PerfilId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
