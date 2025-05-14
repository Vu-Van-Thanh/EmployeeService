using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDateExpired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActived",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateExpired",
                table: "Employees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: new Guid("63e29a28-bc90-4cd1-8f9e-ab9834bdde6c"),
                column: "DateExpired",
                value: null);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: new Guid("d05780d4-5742-40ca-8403-0febd44b1555"),
                column: "DateExpired",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateExpired",
                table: "Employees");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActived",
                table: "Employees",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
