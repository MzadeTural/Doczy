using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doczy.DataAccess.Migrations
{
    public partial class UpdateService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_AspNetUsers_DoctorAppUserId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_DoctorAppUserId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "DoctorAppUserId",
                table: "Services");

            migrationBuilder.AddColumn<Guid>(
                name: "DoctorId",
                table: "Services",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Services_DoctorId",
                table: "Services",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_AspNetUsers_DoctorId",
                table: "Services",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_AspNetUsers_DoctorId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_DoctorId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Services");

            migrationBuilder.AddColumn<Guid>(
                name: "DoctorAppUserId",
                table: "Services",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_DoctorAppUserId",
                table: "Services",
                column: "DoctorAppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_AspNetUsers_DoctorAppUserId",
                table: "Services",
                column: "DoctorAppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
