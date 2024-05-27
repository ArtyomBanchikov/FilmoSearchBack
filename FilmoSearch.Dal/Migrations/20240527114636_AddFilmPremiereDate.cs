using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmoSearch.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddFilmPremiereDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "PremiereDate",
                table: "Films",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PremiereDate",
                table: "Films");
        }
    }
}
