using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doczy.DataAccess.Migrations
{
    public partial class AddTableFavouriteDoctor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoctorRatings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Review = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorRatings_AspNetUsers_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DoctorRatings_AspNetUsers_PatientId",
                        column: x => x.PatientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FavoriteDoctors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteDoctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteDoctors_AspNetUsers_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FavoriteDoctors_AspNetUsers_PatientId",
                        column: x => x.PatientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorRatings_DoctorId",
                table: "DoctorRatings",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorRatings_PatientId",
                table: "DoctorRatings",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteDoctors_DoctorId",
                table: "FavoriteDoctors",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteDoctors_PatientId",
                table: "FavoriteDoctors",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorRatings");

            migrationBuilder.DropTable(
                name: "FavoriteDoctors");
        }
    }
}
