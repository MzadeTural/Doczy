using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doczy.DataAccess.Migrations
{
    public partial class updateExperianceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_WorkPlaces_WorkPlaceId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_WorkPlaceId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WorkPlaceId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Experiances",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "DoctorId",
                table: "Experiances",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Experiances_DoctorId",
                table: "Experiances",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Experiances_AspNetUsers_DoctorId",
                table: "Experiances",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiances_AspNetUsers_DoctorId",
                table: "Experiances");

            migrationBuilder.DropIndex(
                name: "IX_Experiances_DoctorId",
                table: "Experiances");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Experiances");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Experiances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WorkPlaceId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WorkPlaceId",
                table: "AspNetUsers",
                column: "WorkPlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_WorkPlaces_WorkPlaceId",
                table: "AspNetUsers",
                column: "WorkPlaceId",
                principalTable: "WorkPlaces",
                principalColumn: "Id");
        }
    }
}
