using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegrifyLibrary.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dictionaryNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:role", "user,librarian")
                .Annotation("Npgsql:PostgresExtension:hstore", ",,")
                .OldAnnotation("Npgsql:Enum:role", "user,librarian");

            migrationBuilder.AddColumn<Dictionary<string, string>>(
                name: "notification_data",
                table: "notifications",
                type: "hstore",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "notification_data",
                table: "notifications");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:role", "user,librarian")
                .OldAnnotation("Npgsql:Enum:role", "user,librarian")
                .OldAnnotation("Npgsql:PostgresExtension:hstore", ",,");
        }
    }
}
