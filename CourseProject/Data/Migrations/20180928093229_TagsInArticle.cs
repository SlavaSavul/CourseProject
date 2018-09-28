using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CourseProject.Data.Migrations
{
    public partial class TagsInArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Tags");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Tags",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ArticleModelId",
                table: "Tags",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ArticleModelId",
                table: "Tags",
                column: "ArticleModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleTags_ArticleId",
                table: "ArticleTags",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleTags_Articles_ArticleId",
                table: "ArticleTags",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Articles_ArticleModelId",
                table: "Tags",
                column: "ArticleModelId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleTags_Articles_ArticleId",
                table: "ArticleTags");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Articles_ArticleModelId",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_ArticleModelId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_ArticleTags_ArticleId",
                table: "ArticleTags");

            migrationBuilder.DropColumn(
                name: "ArticleModelId",
                table: "Tags");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Tags",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Tags",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");
        }
    }
}
