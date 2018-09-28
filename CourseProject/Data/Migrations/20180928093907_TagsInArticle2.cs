using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CourseProject.Data.Migrations
{
    public partial class TagsInArticle2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Articles_ArticleModelId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_ArticleModelId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "ArticleModelId",
                table: "Tags");

            migrationBuilder.AddColumn<Guid>(
                name: "ArticleId",
                table: "Tags",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ArticleId",
                table: "Tags",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Articles_ArticleId",
                table: "Tags",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Articles_ArticleId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_ArticleId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "Tags");

            migrationBuilder.AddColumn<Guid>(
                name: "ArticleModelId",
                table: "Tags",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ArticleModelId",
                table: "Tags",
                column: "ArticleModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Articles_ArticleModelId",
                table: "Tags",
                column: "ArticleModelId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
