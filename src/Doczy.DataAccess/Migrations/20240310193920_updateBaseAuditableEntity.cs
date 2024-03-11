using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doczy.DataAccess.Migrations
{
    public partial class updateBaseAuditableEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UptadetAt",
                table: "ServiceTypes",
                newName: "LastModifiedDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "ServiceTypes",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "ServiceTypes",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "UptadetAt",
                table: "Services",
                newName: "LastModifiedDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Services",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Services",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "UptadetAt",
                table: "Experiances",
                newName: "LastModifiedDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Experiances",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Experiances",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "UptadetAt",
                table: "Educations",
                newName: "LastModifiedDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Educations",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Educations",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "UptadetAt",
                table: "Blogs",
                newName: "LastModifiedDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Blogs",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Blogs",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "UptadetAt",
                table: "Awards",
                newName: "LastModifiedDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Awards",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Awards",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "UptadetAt",
                table: "Appointments",
                newName: "LastModifiedDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Appointments",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Appointments",
                newName: "CreatedDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastModifiedDate",
                table: "ServiceTypes",
                newName: "UptadetAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "ServiceTypes",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "ServiceTypes",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedDate",
                table: "Services",
                newName: "UptadetAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Services",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Services",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedDate",
                table: "Experiances",
                newName: "UptadetAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Experiances",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Experiances",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedDate",
                table: "Educations",
                newName: "UptadetAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Educations",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Educations",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedDate",
                table: "Blogs",
                newName: "UptadetAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Blogs",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Blogs",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedDate",
                table: "Awards",
                newName: "UptadetAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Awards",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Awards",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedDate",
                table: "Appointments",
                newName: "UptadetAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Appointments",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Appointments",
                newName: "CreatedAt");
        }
    }
}
