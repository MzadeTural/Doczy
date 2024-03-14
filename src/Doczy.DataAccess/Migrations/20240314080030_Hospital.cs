using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doczy.DataAccess.Migrations
{
    public partial class Hospital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiances_WorkPlaces_WorkPlaceId",
                table: "Experiances");

            migrationBuilder.DropTable(
                name: "WorkPlaces");

            migrationBuilder.RenameColumn(
                name: "WorkPlaceId",
                table: "Experiances",
                newName: "HospitalId");

            migrationBuilder.RenameIndex(
                name: "IX_Experiances_WorkPlaceId",
                table: "Experiances",
                newName: "IX_Experiances_HospitalId");

            migrationBuilder.CreateTable(
                name: "Hospitals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IconUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitals", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Experiances_Hospitals_HospitalId",
                table: "Experiances",
                column: "HospitalId",
                principalTable: "Hospitals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiances_Hospitals_HospitalId",
                table: "Experiances");

            migrationBuilder.DropTable(
                name: "Hospitals");

            migrationBuilder.RenameColumn(
                name: "HospitalId",
                table: "Experiances",
                newName: "WorkPlaceId");

            migrationBuilder.RenameIndex(
                name: "IX_Experiances_HospitalId",
                table: "Experiances",
                newName: "IX_Experiances_WorkPlaceId");

            migrationBuilder.CreateTable(
                name: "WorkPlaces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IconUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkPlaces", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Experiances_WorkPlaces_WorkPlaceId",
                table: "Experiances",
                column: "WorkPlaceId",
                principalTable: "WorkPlaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
