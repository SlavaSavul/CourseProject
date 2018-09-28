using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CourseProject.Data.Migrations
{
    public partial class CommentsInArticle2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Articles_ArticleModelId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ArticleModelId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ArticleModelId",
                table: "Comments");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ArticleId",
                table: "Comments",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Articles_ArticleId",
                table: "Comments",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Articles_ArticleId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ArticleId",
                table: "Comments");

            migrationBuilder.AddColumn<Guid>(
                name: "ArticleModelId",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ArticleModelId",
                table: "Comments",
                column: "ArticleModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Articles_ArticleModelId",
                table: "Comments",
                column: "ArticleModelId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
