using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbContextClasses.Migrations
{
    /// <inheritdoc />
    public partial class SecondMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Teacher_Description",
                table: "Teachers",
                newName: "Teacher_Surname");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Teacher_Surname",
                table: "Teachers",
                newName: "Teacher_Description");
        }
    }
}
