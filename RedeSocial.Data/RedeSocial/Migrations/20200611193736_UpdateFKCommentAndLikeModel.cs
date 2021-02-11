using Microsoft.EntityFrameworkCore.Migrations;

namespace RedeSocial.Data.RedeSocial.Migrations
{
    public partial class UpdateFKCommentAndLikeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentPostModel_PostModel_IdPostModelId",
                table: "CommentPostModel");

            migrationBuilder.DropForeignKey(
                name: "FK_LikesPostModel_PostModel_IdPostModelId",
                table: "LikesPostModel");

            migrationBuilder.DropIndex(
                name: "IX_LikesPostModel_IdPostModelId",
                table: "LikesPostModel");

            migrationBuilder.DropIndex(
                name: "IX_CommentPostModel_IdPostModelId",
                table: "CommentPostModel");

            migrationBuilder.DropColumn(
                name: "IdPostModelId",
                table: "LikesPostModel");

            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "LikesPostModel");

            migrationBuilder.DropColumn(
                name: "IdPostModelId",
                table: "CommentPostModel");

            migrationBuilder.AddColumn<int>(
                name: "PostModelId",
                table: "LikesPostModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PostModelId",
                table: "CommentPostModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LikesPostModel_PostModelId",
                table: "LikesPostModel",
                column: "PostModelId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentPostModel_PostModelId",
                table: "CommentPostModel",
                column: "PostModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentPostModel_PostModel_PostModelId",
                table: "CommentPostModel",
                column: "PostModelId",
                principalTable: "PostModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LikesPostModel_PostModel_PostModelId",
                table: "LikesPostModel",
                column: "PostModelId",
                principalTable: "PostModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentPostModel_PostModel_PostModelId",
                table: "CommentPostModel");

            migrationBuilder.DropForeignKey(
                name: "FK_LikesPostModel_PostModel_PostModelId",
                table: "LikesPostModel");

            migrationBuilder.DropIndex(
                name: "IX_LikesPostModel_PostModelId",
                table: "LikesPostModel");

            migrationBuilder.DropIndex(
                name: "IX_CommentPostModel_PostModelId",
                table: "CommentPostModel");

            migrationBuilder.DropColumn(
                name: "PostModelId",
                table: "LikesPostModel");

            migrationBuilder.DropColumn(
                name: "PostModelId",
                table: "CommentPostModel");

            migrationBuilder.AddColumn<int>(
                name: "IdPostModelId",
                table: "LikesPostModel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "LikesPostModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdPostModelId",
                table: "CommentPostModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LikesPostModel_IdPostModelId",
                table: "LikesPostModel",
                column: "IdPostModelId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentPostModel_IdPostModelId",
                table: "CommentPostModel",
                column: "IdPostModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentPostModel_PostModel_IdPostModelId",
                table: "CommentPostModel",
                column: "IdPostModelId",
                principalTable: "PostModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LikesPostModel_PostModel_IdPostModelId",
                table: "LikesPostModel",
                column: "IdPostModelId",
                principalTable: "PostModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
