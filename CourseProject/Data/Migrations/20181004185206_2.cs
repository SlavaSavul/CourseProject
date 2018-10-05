using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CourseProject.Data.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_PersonalAreas_PersonalAreaId",
                table: "AspNetUsers");

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
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PersonalAreaId",
                table: "AspNetUsers",
                column: "PersonalAreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_PersonalAreas_PersonalAreaId",
                table: "AspNetUsers",
                column: "PersonalAreaId",
                principalTable: "PersonalAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
