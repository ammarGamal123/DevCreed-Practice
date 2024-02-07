using System;
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

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedOn",
                schema: "blogging",
                table: "blogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedOn",
                schema: "blogging",
                table: "blogs");

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
