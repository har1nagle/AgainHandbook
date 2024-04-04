using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Again.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddLibraryInRow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Libraries",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[] { 4, 2, " try n one more file for test" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Libraries",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
