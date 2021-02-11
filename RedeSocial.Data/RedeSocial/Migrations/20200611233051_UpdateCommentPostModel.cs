using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RedeSocial.Data.RedeSocial.Migrations
{
    public partial class UpdateCommentPostModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataDoComment",
                table: "CommentPostModel",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataDoComment",
                table: "CommentPostModel");
        }
    }
}
