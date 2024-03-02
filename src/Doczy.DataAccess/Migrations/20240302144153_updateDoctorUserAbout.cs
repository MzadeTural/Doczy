using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doczy.DataAccess.Migrations
{
    public partial class updateDoctorUserAbout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutDoctor",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutDoctor",
                table: "AspNetUsers");
        }
    }
}
