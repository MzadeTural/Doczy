using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doczy.DataAccess.Migrations
{
    public partial class updateTempApoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChosenHourId",
                table: "TempAppointments",
                newName: "PatientId");

            migrationBuilder.RenameColumn(
                name: "ChosenDate",
                table: "TempAppointments",
                newName: "AppointmentDate");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "AppointmentTime",
                table: "TempAppointments",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentTime",
                table: "TempAppointments");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "TempAppointments",
                newName: "ChosenHourId");

            migrationBuilder.RenameColumn(
                name: "AppointmentDate",
                table: "TempAppointments",
                newName: "ChosenDate");
        }
    }
}
