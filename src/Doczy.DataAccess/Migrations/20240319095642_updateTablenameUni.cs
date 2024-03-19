using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doczy.DataAccess.Migrations
{
    public partial class updateTablenameUni : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educations_Univercities_UnivercityId",
                table: "Educations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Univercities",
                table: "Univercities");

            migrationBuilder.RenameTable(
                name: "Univercities",
                newName: "Universities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Universities",
                table: "Universities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_Universities_UnivercityId",
                table: "Educations",
                column: "UnivercityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educations_Universities_UnivercityId",
                table: "Educations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Universities",
                table: "Universities");

            migrationBuilder.RenameTable(
                name: "Universities",
                newName: "Univercities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Univercities",
                table: "Univercities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_Univercities_UnivercityId",
                table: "Educations",
                column: "UnivercityId",
                principalTable: "Univercities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
