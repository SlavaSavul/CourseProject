using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CourseProject.Data.Migrations
{
    public partial class _1231 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_PersonalAreas_PersonalAreaModelId",
                table: "Articles");

            migrationBuilder.DropTable(
                name: "PersonalAreas");

            migrationBuilder.DropIndex(
                name: "IX_Articles_PersonalAreaModelId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "PersonalAreaModelId",
                table: "Articles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PersonalAreaModelId",
                table: "Articles",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PersonalAreas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalAreas_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_PersonalAreaModelId",
                table: "Articles",
                column: "PersonalAreaModelId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalAreas_UserId",
                table: "PersonalAreas",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_PersonalAreas_PersonalAreaModelId",
                table: "Articles",
                column: "PersonalAreaModelId",
                principalTable: "PersonalAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
