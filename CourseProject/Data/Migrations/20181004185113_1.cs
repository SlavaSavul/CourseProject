using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CourseProject.Data.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleListViewModel");

            migrationBuilder.AddColumn<Guid>(
                name: "PersonalAreaId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PersonalAreaModelId",
                table: "Articles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PersonalAreaId",
                table: "AspNetUsers",
                column: "PersonalAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_PersonalAreaModelId",
                table: "Articles",
                column: "PersonalAreaModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_PersonalAreas_PersonalAreaModelId",
                table: "Articles",
                column: "PersonalAreaModelId",
                principalTable: "PersonalAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_PersonalAreas_PersonalAreaId",
                table: "AspNetUsers",
                column: "PersonalAreaId",
                principalTable: "PersonalAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_PersonalAreas_PersonalAreaModelId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_PersonalAreas_PersonalAreaId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PersonalAreaId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_Articles_PersonalAreaModelId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "PersonalAreaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PersonalAreaModelId",
                table: "Articles");

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
    }
}
