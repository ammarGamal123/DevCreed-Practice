using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Migrations
{
    /// <inheritdoc />
    public partial class AddAddedOnToBlogsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "blogs",
                newName: "blogs",
                newSchema: "blogging");

            migrationBuilder.RenameTable(
                name: "AuditEntry",
                newName: "AuditEntry",
                newSchema: "blogging");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "blogs",
                schema: "blogging",
                newName: "blogs");

            migrationBuilder.RenameTable(
                name: "AuditEntry",
                schema: "blogging",
                newName: "AuditEntry");
        }
    }
}
