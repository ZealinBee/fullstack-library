using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegrifyLibrary.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class bookToLoanDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_loan_details_book_id",
                table: "loan_details",
                column: "book_id");

            migrationBuilder.AddForeignKey(
                name: "fk_loan_details_books_book_id",
                table: "loan_details",
                column: "book_id",
                principalTable: "books",
                principalColumn: "book_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_loan_details_books_book_id",
                table: "loan_details");

            migrationBuilder.DropIndex(
                name: "ix_loan_details_book_id",
                table: "loan_details");
        }
    }
}
