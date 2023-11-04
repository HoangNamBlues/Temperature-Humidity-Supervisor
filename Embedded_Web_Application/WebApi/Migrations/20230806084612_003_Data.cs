using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class _003_Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Time",
                table: "Temperatures",
                newName: "TemperatureTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "HumidTime",
                table: "Humidities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Humidities",
                columns: new[] { "HumidityId", "HumidTime", "HumidityValue" },
                values: new object[] { new Guid("bf0eb6c2-16d4-486b-87ed-b915f85d1fe5"), new DateTime(2023, 8, 6, 15, 46, 12, 285, DateTimeKind.Local).AddTicks(2828), 93.25 });

            migrationBuilder.InsertData(
                table: "Temperatures",
                columns: new[] { "TemperatureId", "TemperatureTime", "TemperatureValue" },
                values: new object[] { new Guid("8516d663-e99c-408e-ae55-752b5abf6905"), new DateTime(2023, 8, 6, 15, 46, 12, 285, DateTimeKind.Local).AddTicks(2816), 26.550000000000001 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Humidities",
                keyColumn: "HumidityId",
                keyValue: new Guid("bf0eb6c2-16d4-486b-87ed-b915f85d1fe5"));

            migrationBuilder.DeleteData(
                table: "Temperatures",
                keyColumn: "TemperatureId",
                keyValue: new Guid("8516d663-e99c-408e-ae55-752b5abf6905"));

            migrationBuilder.DropColumn(
                name: "HumidTime",
                table: "Humidities");

            migrationBuilder.RenameColumn(
                name: "TemperatureTime",
                table: "Temperatures",
                newName: "Time");
        }
    }
}
