using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CourseProject.Data.Migrations
{
    public partial class _1223 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonalAreas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AspNetUserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalAreas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleListViewModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ModifitedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PersonalAreaModelId = table.Column<Guid>(nullable: true),
                    Rate = table.Column<double>(nullable: false),
                    Speciality = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleListViewModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleListViewModel_PersonalAreas_PersonalAreaModelId",
                        column: x => x.PersonalAreaModelId,
                        principalTable: "PersonalAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleListViewModel_PersonalAreaModelId",
                table: "ArticleListViewModel",
                column: "PersonalAreaModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleListViewModel");

            migrationBuilder.DropTable(
                name: "PersonalAreas");
        }
    }
}
