using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class _002_Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Humidities",
                keyColumn: "HumidityId",
                keyValue: new Guid("b7d38156-9955-4a36-ad14-673fb9b14c96"));

            migrationBuilder.DeleteData(
                table: "Temperatures",
                keyColumn: "TemperatureId",
                keyValue: new Guid("cfa33a92-49d8-44dc-9546-9bf28ef73cdc"));

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "Temperatures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "Temperatures");

            migrationBuilder.InsertData(
                table: "Humidities",
                columns: new[] { "HumidityId", "HumidityValue" },
                values: new object[] { new Guid("b7d38156-9955-4a36-ad14-673fb9b14c96"), 93.25 });

            migrationBuilder.InsertData(
                table: "Temperatures",
                columns: new[] { "TemperatureId", "TemperatureValue" },
                values: new object[] { new Guid("cfa33a92-49d8-44dc-9546-9bf28ef73cdc"), 26.550000000000001 });
        }
    }
}
