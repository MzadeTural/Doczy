using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doczy.DataAccess.Migrations
{
    public partial class CreateRelationDoctorCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DoctorCategoryId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DoctorCategoryId",
                table: "AspNetUsers",
                column: "DoctorCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_DoctorCategories_DoctorCategoryId",
                table: "AspNetUsers",
                column: "DoctorCategoryId",
                principalTable: "DoctorCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_DoctorCategories_DoctorCategoryId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DoctorCategoryId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DoctorCategoryId",
                table: "AspNetUsers");
        }
    }
}
