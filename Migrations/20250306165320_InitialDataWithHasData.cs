using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieApplication.Migrations
{
    /// <inheritdoc />
    public partial class InitialDataWithHasData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "app",
                table: "Movies",
                columns: new[] { "Id", "Created", "Genre", "Rating", "ReleseDate", "Title" },
                values: new object[] { new Guid("01c681ed-278c-444c-b53c-917cefa3b02c"), new DateTimeOffset(new DateTime(2025, 3, 6, 16, 53, 19, 566, DateTimeKind.Unspecified).AddTicks(3873), new TimeSpan(0, 0, 0, 0, 0)), "Fantasy", 8.0, new DateTimeOffset(new DateTime(2025, 3, 6, 16, 53, 19, 566, DateTimeKind.Unspecified).AddTicks(5564), new TimeSpan(0, 0, 0, 0, 0)), "Sonic the hedgeh 3" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "app",
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("01c681ed-278c-444c-b53c-917cefa3b02c"));
        }
    }
}
