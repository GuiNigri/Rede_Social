using Microsoft.EntityFrameworkCore.Migrations;

namespace RedeSocial.Data.RedeSocial.Migrations
{
    public partial class createPostCommentAndLike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommentPostModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityUser = table.Column<string>(nullable: true),
                    IdPostModelId = table.Column<int>(nullable: true),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentPostModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentPostModel_PostModel_IdPostModelId",
                        column: x => x.IdPostModelId,
                        principalTable: "PostModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LikesPostModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityUser = table.Column<string>(nullable: true),
                    IdPostModelId = table.Column<int>(nullable: true),
                    MyProperty = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikesPostModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikesPostModel_PostModel_IdPostModelId",
                        column: x => x.IdPostModelId,
                        principalTable: "PostModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentPostModel_IdPostModelId",
                table: "CommentPostModel",
                column: "IdPostModelId");

            migrationBuilder.CreateIndex(
                name: "IX_LikesPostModel_IdPostModelId",
                table: "LikesPostModel",
                column: "IdPostModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentPostModel");

            migrationBuilder.DropTable(
                name: "LikesPostModel");
        }
    }
}
