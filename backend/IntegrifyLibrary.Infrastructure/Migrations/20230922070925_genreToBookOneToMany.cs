using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegrifyLibrary.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class genreToBookOneToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "book_genres");

            migrationBuilder.AddColumn<Guid>(
                name: "book_id",
                table: "genres",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_genres_book_id",
                table: "genres",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "ix_books_genre_id",
                table: "books",
                column: "genre_id");

            migrationBuilder.AddForeignKey(
                name: "fk_books_genres_genre_id",
                table: "books",
                column: "genre_id",
                principalTable: "genres",
                principalColumn: "genre_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_genres_books_book_id",
                table: "genres",
                column: "book_id",
                principalTable: "books",
                principalColumn: "book_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_books_genres_genre_id",
                table: "books");

            migrationBuilder.DropForeignKey(
                name: "fk_genres_books_book_id",
                table: "genres");

            migrationBuilder.DropIndex(
                name: "ix_genres_book_id",
                table: "genres");

            migrationBuilder.DropIndex(
                name: "ix_books_genre_id",
                table: "books");

            migrationBuilder.DropColumn(
                name: "book_id",
                table: "genres");

            migrationBuilder.CreateTable(
                name: "book_genres",
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
                table: "book_genres",
                column: "genres_genre_id");
        }
    }
}
