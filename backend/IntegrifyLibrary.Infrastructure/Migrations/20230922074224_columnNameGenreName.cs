using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegrifyLibrary.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class columnNameGenreName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "genre_name",
                table: "books",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "genre_name",
                table: "books");
        }
    }
}
