using Microsoft.EntityFrameworkCore.Migrations;

namespace RedeSocial.Apresentacao.Areas.Identity.Data.Migrations
{
    public partial class CreateDBLONG : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Cpf",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Cpf",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
