using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmoSearch.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddFilmDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Films",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Films");
        }
    }
}
