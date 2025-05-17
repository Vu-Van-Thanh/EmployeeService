using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyRelative : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Relatives_EmployeeID",
                table: "Relatives",
                column: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Relatives_Employees_EmployeeID",
                table: "Relatives",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relatives_Employees_EmployeeID",
                table: "Relatives");

            migrationBuilder.DropIndex(
                name: "IX_Relatives_EmployeeID",
                table: "Relatives");
        }
    }
}
