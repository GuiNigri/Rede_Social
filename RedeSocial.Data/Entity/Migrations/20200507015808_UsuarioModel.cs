using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RedeSocial.Data.Entity.Migrations
{
    public partial class UsuarioModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Sobrenome = table.Column<string>(nullable: true),
                    Cpf = table.Column<long>(nullable: false),
                    DataNascimento = table.Column<DateTime>(nullable: false),
                    FotoPerfil = table.Column<string>(nullable: true),
                    PerfilId = table.Column<int>(nullable: false),
                    PerfilId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_AspNetUsers_PerfilId1",
                        column: x => x.PerfilId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PerfilId1",
                table: "Invoices",
                column: "PerfilId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoices");
        }
    }
}
