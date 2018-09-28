using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CourseProject.Data.Migrations
{
    public partial class MarksInArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AricleId",
                table: "Marks",
                newName: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Marks_ArticleId",
                table: "Marks",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_CommentId",
                table: "Likes",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Comments_CommentId",
                table: "Likes",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_Articles_ArticleId",
                table: "Marks",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Comments_CommentId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Marks_Articles_ArticleId",
                table: "Marks");

            migrationBuilder.DropIndex(
                name: "IX_Marks_ArticleId",
                table: "Marks");

            migrationBuilder.DropIndex(
                name: "IX_Likes_CommentId",
                table: "Likes");

            migrationBuilder.RenameColumn(
                name: "ArticleId",
                table: "Marks",
                newName: "AricleId");
        }
    }
}
