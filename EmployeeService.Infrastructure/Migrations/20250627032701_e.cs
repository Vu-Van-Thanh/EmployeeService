using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class e : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DetailJsonManager",
                table: "EmployeeEvaluations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EmployeeEvaluations",
                keyColumn: "ID",
                keyValue: new Guid("a4e3f08f-b6df-4d58-8377-8b97fc5cf66e"),
                column: "DetailJsonManager",
                value: null);

            migrationBuilder.UpdateData(
                table: "EmployeeEvaluations",
                keyColumn: "ID",
                keyValue: new Guid("cca92ac7-7e25-4a8c-8f62-db3521909f07"),
                column: "DetailJsonManager",
                value: null);

            migrationBuilder.UpdateData(
                table: "EmployeeEvaluations",
                keyColumn: "ID",
                keyValue: new Guid("d6b2d82a-1f74-4b5a-bf7e-638e5c6e366a"),
                column: "DetailJsonManager",
                value: null);

            migrationBuilder.UpdateData(
                table: "EmployeeEvaluations",
                keyColumn: "ID",
                keyValue: new Guid("e5a9b9d3-04a5-4a2b-9e38-78e7b250b4df"),
                column: "DetailJsonManager",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DetailJsonManager",
                table: "EmployeeEvaluations");
        }
    }
}
