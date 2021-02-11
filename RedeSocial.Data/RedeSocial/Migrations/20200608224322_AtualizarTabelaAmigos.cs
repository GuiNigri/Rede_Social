using Microsoft.EntityFrameworkCore.Migrations;

namespace RedeSocial.Data.RedeSocial.Migrations
{
    public partial class AtualizarTabelaAmigos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusAmizade",
                table: "AmigosModel",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusAmizade",
                table: "AmigosModel");
        }
    }
}
