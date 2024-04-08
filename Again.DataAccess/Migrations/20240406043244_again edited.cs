using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Again.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class againedited : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Libraries",
                newName: "SelectedDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SelectedDate",
                table: "Libraries",
                newName: "DateOfBirth");
        }
    }
}
