using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieApplication.Migrations
{
    /// <inheritdoc />
    public partial class newMg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "director_Id",
                schema: "app",
                table: "Movies",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "movie_Id",
                schema: "app",
                table: "Movies",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Commands",
                schema: "app",
                columns: table => new
                {
                    command_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    movie_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    command = table.Column<string>(type: "text", nullable: false),
                    created_At = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commands", x => x.command_Id);
                    table.ForeignKey(
                        name: "FK_Commands_Movies_movie_Id",
                        column: x => x.movie_Id,
                        principalSchema: "app",
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Commands_Users_user_Id",
                        column: x => x.user_Id,
                        principalSchema: "app",
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commands_movie_Id",
                schema: "app",
                table: "Commands",
                column: "movie_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Commands_user_Id",
                schema: "app",
                table: "Commands",
                column: "user_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commands",
                schema: "app");

            migrationBuilder.DropColumn(
                name: "director_Id",
                schema: "app",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "movie_Id",
                schema: "app",
                table: "Movies");
        }
    }
}
