using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyContracts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContractUrl",
                table: "EmployeeContracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EmployeeContracts",
                keyColumn: "ContractId",
                keyValue: new Guid("9ce6d105-3f65-432e-85f0-714299eb6644"),
                column: "ContractUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "EmployeeContracts",
                keyColumn: "ContractId",
                keyValue: new Guid("c3f4dd5a-d9fb-4df8-ac02-b7128fd52271"),
                column: "ContractUrl",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractUrl",
                table: "EmployeeContracts");
        }
    }
}
