using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegrifyLibrary.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class loanRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "loan_details",
                columns: table => new
                {
                    loan_details_id = table.Column<Guid>(type: "uuid", nullable: false),
                    loan_id = table.Column<Guid>(type: "uuid", nullable: false),
                    book_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_loan_details", x => x.loan_details_id);
                    table.ForeignKey(
                        name: "fk_loan_details_loans_loan_id",
                        column: x => x.loan_id,
                        principalTable: "loans",
                        principalColumn: "loan_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_loans_user_id",
                table: "loans",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_loan_details_loan_id",
                table: "loan_details",
                column: "loan_id");

            migrationBuilder.AddForeignKey(
                name: "fk_loans_users_user_id",
                table: "loans",
                column: "user_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_loans_users_user_id",
                table: "loans");

            migrationBuilder.DropTable(
                name: "loan_details");

            migrationBuilder.DropIndex(
                name: "ix_loans_user_id",
                table: "loans");
        }
    }
}
