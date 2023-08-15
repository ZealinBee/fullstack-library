using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegrifyLibrary.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MigrationTesting2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "users",
                type: "text",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "bytea");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "password",
                table: "users",
                type: "bytea",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
