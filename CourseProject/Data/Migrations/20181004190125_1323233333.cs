using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CourseProject.Data.Migrations
{
    public partial class _1323233333 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AspNetUserId",
                table: "PersonalAreas",
                newName: "UserId");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "PersonalAreas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalAreas_UserId1",
                table: "PersonalAreas",
                column: "UserId1",
                unique: true,
                filter: "[UserId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalAreas_AspNetUsers_UserId1",
                table: "PersonalAreas",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalAreas_AspNetUsers_UserId1",
                table: "PersonalAreas");

            migrationBuilder.DropIndex(
                name: "IX_PersonalAreas_UserId1",
                table: "PersonalAreas");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "PersonalAreas");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "PersonalAreas",
                newName: "AspNetUserId");
        }
    }
}
