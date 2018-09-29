using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CourseProject.Data.Migrations
{
    public partial class ArticleTagInArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Articles_ArticleId",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_ArticleId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "ArticleId",
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

            migrationBuilder.CreateIndex(
                name: "IX_ArticleTags_TagId",
                table: "ArticleTags",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleTags_Tags_TagId",
                table: "ArticleTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleTags_Tags_TagId",
                table: "ArticleTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_ArticleTags_TagId",
                table: "ArticleTags");

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
                name: "ArticleId",
                table: "Tags",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Title");

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
    }
}
