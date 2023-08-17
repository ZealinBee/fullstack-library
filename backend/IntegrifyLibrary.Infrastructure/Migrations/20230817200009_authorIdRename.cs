using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegrifyLibrary.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class authorIdRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "books",
                newName: "book_id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "authors",
                newName: "author_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "book_id",
                table: "books",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "author_id",
                table: "authors",
                newName: "id");
        }
    }
}
