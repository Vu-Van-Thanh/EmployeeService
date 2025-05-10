using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyContracts2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "EmployeeContracts",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "EmployeeContracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "SalaryBase",
                table: "EmployeeContracts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "EmployeeContracts",
                keyColumn: "ContractId",
                keyValue: new Guid("9ce6d105-3f65-432e-85f0-714299eb6644"),
                columns: new[] { "Position", "SalaryBase" },
                values: new object[] { "Software Engineer", 50000m });

            migrationBuilder.UpdateData(
                table: "EmployeeContracts",
                keyColumn: "ContractId",
                keyValue: new Guid("c3f4dd5a-d9fb-4df8-ac02-b7128fd52271"),
                columns: new[] { "Position", "SalaryBase" },
                values: new object[] { "Architecture Engineer", 50000m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "EmployeeContracts");

            migrationBuilder.DropColumn(
                name: "SalaryBase",
                table: "EmployeeContracts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "EmployeeContracts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
