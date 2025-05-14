using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActived",
                table: "Employees",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: new Guid("63e29a28-bc90-4cd1-8f9e-ab9834bdde6c"),
                column: "IsActived",
                value: true);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: new Guid("d05780d4-5742-40ca-8403-0febd44b1555"),
                column: "IsActived",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActived",
                table: "Employees");
        }
    }
}
