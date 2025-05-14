using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDateIssued : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateIssued",
                table: "Employees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: new Guid("63e29a28-bc90-4cd1-8f9e-ab9834bdde6c"),
                column: "DateIssued",
                value: new DateTime(2015, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: new Guid("d05780d4-5742-40ca-8403-0febd44b1555"),
                column: "DateIssued",
                value: new DateTime(2016, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateIssued",
                table: "Employees");
        }
    }
}
