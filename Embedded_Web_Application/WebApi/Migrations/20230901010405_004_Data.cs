using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class _004_Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Humidities",
                keyColumn: "HumidityId",
                keyValue: new Guid("cf3e5faa-b4cd-4813-85c0-1cf6b2558cdc"));

            migrationBuilder.DeleteData(
                table: "Temperatures",
                keyColumn: "TemperatureId",
                keyValue: new Guid("b1235641-4e95-40c7-9c78-1540146e3377"));

            migrationBuilder.InsertData(
                table: "Humidities",
                columns: new[] { "HumidityId", "HumidityTime", "HumidityValue" },
                values: new object[] { new Guid("ccef2a9f-fe68-49c8-b949-12ef9943de6b"), new DateTime(2023, 9, 1, 8, 4, 5, 69, DateTimeKind.Local).AddTicks(8301), 93.25 });

            migrationBuilder.InsertData(
                table: "Temperatures",
                columns: new[] { "TemperatureId", "TemperatureTime", "TemperatureValue" },
                values: new object[] { new Guid("4270424e-1b48-4207-9c03-db4cfcb6a981"), new DateTime(2023, 9, 1, 8, 4, 5, 69, DateTimeKind.Local).AddTicks(8289), 26.550000000000001 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Humidities",
                keyColumn: "HumidityId",
                keyValue: new Guid("ccef2a9f-fe68-49c8-b949-12ef9943de6b"));

            migrationBuilder.DeleteData(
                table: "Temperatures",
                keyColumn: "TemperatureId",
                keyValue: new Guid("4270424e-1b48-4207-9c03-db4cfcb6a981"));

            migrationBuilder.InsertData(
                table: "Humidities",
                columns: new[] { "HumidityId", "HumidityTime", "HumidityValue" },
                values: new object[] { new Guid("cf3e5faa-b4cd-4813-85c0-1cf6b2558cdc"), new DateTime(2023, 8, 31, 23, 43, 59, 902, DateTimeKind.Local).AddTicks(6401), 93.25 });

            migrationBuilder.InsertData(
                table: "Temperatures",
                columns: new[] { "TemperatureId", "TemperatureTime", "TemperatureValue" },
                values: new object[] { new Guid("b1235641-4e95-40c7-9c78-1540146e3377"), new DateTime(2023, 8, 31, 23, 43, 59, 902, DateTimeKind.Local).AddTicks(6390), 26.550000000000001 });
        }
    }
}
