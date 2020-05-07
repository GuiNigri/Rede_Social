using Microsoft.EntityFrameworkCore.Migrations;

namespace RedeSocial.Identity.Migrations
{
    public partial class fk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosDB_AspNetUsers_IdentityUserId1",
                table: "UsuariosDB");

            migrationBuilder.DropIndex(
                name: "IX_UsuariosDB_IdentityUserId1",
                table: "UsuariosDB");

            migrationBuilder.DropColumn(
                name: "IdentityUserId1",
                table: "UsuariosDB");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityUserId",
                table: "UsuariosDB",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosDB_IdentityUserId",
                table: "UsuariosDB",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosDB_AspNetUsers_IdentityUserId",
                table: "UsuariosDB",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosDB_AspNetUsers_IdentityUserId",
                table: "UsuariosDB");

            migrationBuilder.DropIndex(
                name: "IX_UsuariosDB_IdentityUserId",
                table: "UsuariosDB");

            migrationBuilder.AlterColumn<int>(
                name: "IdentityUserId",
                table: "UsuariosDB",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId1",
                table: "UsuariosDB",
                type: "nvarchar(450)",
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
    }
}
