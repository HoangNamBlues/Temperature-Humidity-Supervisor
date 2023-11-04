using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class Roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("777eee96-8578-4dfb-9c13-158e5d5d8a9e"), "777eee96-8578-4dfb-9c13-158e5d5d8a9e", "Admin", "ADMIN" },
                    { new Guid("a68985b2-1f1b-468d-bc6d-61288ed2a07e"), "a68985b2-1f1b-468d-bc6d-61288ed2a07e", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("777eee96-8578-4dfb-9c13-158e5d5d8a9e"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a68985b2-1f1b-468d-bc6d-61288ed2a07e"));
        }
    }
}
