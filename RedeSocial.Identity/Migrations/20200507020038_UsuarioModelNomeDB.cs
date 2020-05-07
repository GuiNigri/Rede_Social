using Microsoft.EntityFrameworkCore.Migrations;

namespace RedeSocial.Identity.Migrations
{
    public partial class UsuarioModelNomeDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_AspNetUsers_PerfilId1",
                table: "Invoices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices");

            migrationBuilder.RenameTable(
                name: "Invoices",
                newName: "UsuariosDB");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_PerfilId1",
                table: "UsuariosDB",
                newName: "IX_UsuariosDB_PerfilId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuariosDB",
                table: "UsuariosDB",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosDB_AspNetUsers_PerfilId1",
                table: "UsuariosDB",
                column: "PerfilId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosDB_AspNetUsers_PerfilId1",
                table: "UsuariosDB");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuariosDB",
                table: "UsuariosDB");

            migrationBuilder.RenameTable(
                name: "UsuariosDB",
                newName: "Invoices");

            migrationBuilder.RenameIndex(
                name: "IX_UsuariosDB_PerfilId1",
                table: "Invoices",
                newName: "IX_Invoices_PerfilId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_AspNetUsers_PerfilId1",
                table: "Invoices",
                column: "PerfilId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
