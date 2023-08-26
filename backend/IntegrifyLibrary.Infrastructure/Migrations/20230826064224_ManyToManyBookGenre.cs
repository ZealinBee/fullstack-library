using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegrifyLibrary.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ManyToManyBookGenre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "book_id",
                table: "loans");

            migrationBuilder.CreateTable(
                name: "genres",
                columns: table => new
                {
                    genre_id = table.Column<Guid>(type: "uuid", nullable: false),
                    genre_name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateOnly>(type: "date", nullable: false),
                    modified_at = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_genres", x => x.genre_id);
                });

            migrationBuilder.CreateTable(
                name: "BookGenres",
                columns: table => new
                {
                    books_book_id = table.Column<Guid>(type: "uuid", nullable: false),
                    genres_genre_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_book_genres", x => new { x.books_book_id, x.genres_genre_id });
                    table.ForeignKey(
                        name: "fk_book_genres_books_books_book_id",
                        column: x => x.books_book_id,
                        principalTable: "books",
                        principalColumn: "book_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_book_genres_genres_genres_genre_id",
                        column: x => x.genres_genre_id,
                        principalTable: "genres",
                        principalColumn: "genre_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_book_genres_genres_genre_id",
                table: "BookGenres",
                column: "genres_genre_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookGenres");

            migrationBuilder.DropTable(
                name: "genres");

            migrationBuilder.AddColumn<Guid>(
                name: "book_id",
                table: "loans",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
