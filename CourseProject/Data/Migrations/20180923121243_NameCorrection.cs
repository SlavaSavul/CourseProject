using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CourseProject.Data.Migrations
{
    public partial class NameCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AbstractId",
                table: "ArticleTags",
                newName: "ArticleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ArticleId",
                table: "ArticleTags",
                newName: "AbstractId");
        }
    }
}
