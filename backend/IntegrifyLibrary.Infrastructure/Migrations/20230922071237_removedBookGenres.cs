using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegrifyLibrary.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removedBookGenres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_genres_books_book_id",
                table: "genres");

            migrationBuilder.DropIndex(
                name: "ix_genres_book_id",
                table: "genres");

            migrationBuilder.DropColumn(
                name: "book_id",
                table: "genres");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "book_id",
                table: "genres",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_genres_book_id",
                table: "genres",
                column: "book_id");

            migrationBuilder.AddForeignKey(
                name: "fk_genres_books_book_id",
                table: "genres",
                column: "book_id",
                principalTable: "books",
                principalColumn: "book_id");
        }
    }
}
