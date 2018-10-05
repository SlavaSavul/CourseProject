using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CourseProject.Data.Migrations
{
    public partial class Last : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_PersonalAreaModel_PersonalAreaId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ArticleListViewModel");

            migrationBuilder.DropTable(
                name: "PersonalAreaModel");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PersonalAreaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PersonalAreaId",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PersonalAreaId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PersonalAreaModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AspNetUserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalAreaModel", x => x.Id);
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
                        name: "FK_ArticleListViewModel_PersonalAreaModel_PersonalAreaModelId",
                        column: x => x.PersonalAreaModelId,
                        principalTable: "PersonalAreaModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PersonalAreaId",
                table: "AspNetUsers",
                column: "PersonalAreaId",
                unique: true,
                filter: "[PersonalAreaId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleListViewModel_PersonalAreaModelId",
                table: "ArticleListViewModel",
                column: "PersonalAreaModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_PersonalAreaModel_PersonalAreaId",
                table: "AspNetUsers",
                column: "PersonalAreaId",
                principalTable: "PersonalAreaModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
