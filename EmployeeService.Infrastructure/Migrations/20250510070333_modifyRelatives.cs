using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class modifyRelatives : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Relatives",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Relatives",
                keyColumn: "RelativeID",
                keyValue: new Guid("77f52741-e1d4-4fe5-ad46-efa6ebf2cee6"),
                column: "PhoneNumber",
                value: null);

            migrationBuilder.UpdateData(
                table: "Relatives",
                keyColumn: "RelativeID",
                keyValue: new Guid("c46795c8-45e3-498f-bbf0-aa8b7e76da0e"),
                column: "PhoneNumber",
                value: null);

            migrationBuilder.UpdateData(
                table: "Relatives",
                keyColumn: "RelativeID",
                keyValue: new Guid("c48e9c2e-6a7e-4e83-9a1e-60ee0a6f30a2"),
                column: "PhoneNumber",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Relatives");
        }
    }
}
