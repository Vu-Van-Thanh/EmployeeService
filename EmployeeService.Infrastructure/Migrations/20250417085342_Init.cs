using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DepartmentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<int>(type: "int", maxLength: 10, nullable: true),
                    Tax = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Ethnic = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Religion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PlaceOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndentityCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceIssued = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    District = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Commune = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    InsuranceNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeID);
                });

            migrationBuilder.CreateTable(
                name: "Relatives",
                columns: table => new
                {
                    RelativeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    RelativeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Ethnic = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Religion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PlaceOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndentityCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    District = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Commune = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relatives", x => x.RelativeID);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeContracts",
                columns: table => new
                {
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContractNumber = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    ContractType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SalaryIndex = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeContracts", x => x.ContractId);
                    table.ForeignKey(
                        name: "FK_EmployeeContracts_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeMedias",
                columns: table => new
                {
                    EmployeeMediaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MediaType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MediaUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeMedias", x => x.EmployeeMediaID);
                    table.ForeignKey(
                        name: "FK_EmployeeMedias_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeID", "AccountID", "Address", "Commune", "Country", "DateOfBirth", "DepartmentID", "District", "Ethnic", "FirstName", "Gender", "IndentityCard", "InsuranceNumber", "LastName", "Nationality", "PlaceIssued", "PlaceOfBirth", "Position", "Province", "Religion", "Tax" },
                values: new object[,]
                {
                    { new Guid("63e29a28-bc90-4cd1-8f9e-ab9834bdde6c"), new Guid("30e50c07-64b5-4adc-bd04-dfb693a3928c"), "123 Le Loi, District 1, Ho Chi Minh City", "Ben Nghe", "Vietnam", new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "District 1", "Kinh", "Nguyen", 0, "079203456789", "VN1234567890", "Van A", "Việt Nam", "HCMC", "Ho Chi Minh City", "CK000", "Ho Chi Minh", "Không", "1234567890123" },
                    { new Guid("d05780d4-5742-40ca-8403-0febd44b1555"), null, "45 Nguyen Hue, District 1, Ho Chi Minh City", "Hang Bac", "Vietnam", new DateTime(1995, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hoan Kiem", "Kinh", "Tran", 1, "091234567890", "VN0987654321", "Thi B", "Việt Nam", "Ha Noi", "Ha Noi", "NH000", "Ha Noi", "Phật giáo", "9876543210987" }
                });

            migrationBuilder.InsertData(
                table: "Relatives",
                columns: new[] { "RelativeID", "Address", "Commune", "Country", "DateOfBirth", "District", "EmployeeID", "Ethnic", "FirstName", "IndentityCard", "LastName", "Nationality", "PlaceOfBirth", "Province", "RelativeType", "Religion" },
                values: new object[,]
                {
                    { new Guid("77f52741-e1d4-4fe5-ad46-efa6ebf2cee6"), "123 Đường Lê Lợi, Hà Nội", "Tràng Tiền", "Việt Nam", new DateTime(1970, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hoàn Kiếm", new Guid("63e29a28-bc90-4cd1-8f9e-ab9834bdde6c"), "Kinh", "Nguyễn", "123456789", "Văn A", "Việt Nam", "Hà Nội", "Hà Nội", "Bố", "Không" },
                    { new Guid("c46795c8-45e3-498f-bbf0-aa8b7e76da0e"), "456 Đường Nguyễn Trãi, TP.HCM", "Bến Nghé", "Việt Nam", new DateTime(1972, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Quận 1", new Guid("63e29a28-bc90-4cd1-8f9e-ab9834bdde6c"), "Kinh", "Trần", "987654321", "Thị B", "Việt Nam", "TP.HCM", "TP.HCM", "Mẹ", "Không" },
                    { new Guid("c48e9c2e-6a7e-4e83-9a1e-60ee0a6f30a2"), "789 Đường Láng, Hà Nội", "Láng Thượng", "Việt Nam", new DateTime(1995, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đống Đa", new Guid("63e29a28-bc90-4cd1-8f9e-ab9834bdde6c"), "Kinh", "Phạm", "456789123", "Văn C", "Việt Nam", "Hà Nội", "Hà Nội", "Anh trai", "Không" }
                });

            migrationBuilder.InsertData(
                table: "EmployeeContracts",
                columns: new[] { "ContractId", "ContractNumber", "ContractType", "EmployeeId", "EndDate", "SalaryIndex", "StartDate", "Status" },
                values: new object[,]
                {
                    { new Guid("9ce6d105-3f65-432e-85f0-714299eb6644"), "CT0001", "Full-time", new Guid("63e29a28-bc90-4cd1-8f9e-ab9834bdde6c"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1.5m, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active" },
                    { new Guid("c3f4dd5a-d9fb-4df8-ac02-b7128fd52271"), "CT0002", "Part-time", new Guid("d05780d4-5742-40ca-8403-0febd44b1555"), null, 1.2m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active" }
                });

            migrationBuilder.InsertData(
                table: "EmployeeMedias",
                columns: new[] { "EmployeeMediaID", "EmployeeID", "MediaType", "MediaUrl" },
                values: new object[,]
                {
                    { new Guid("674d4176-7d33-44fb-a198-583028016a62"), new Guid("d05780d4-5742-40ca-8403-0febd44b1555"), "AVATAR", "/wwwroot/Asserts/avatar.png" },
                    { new Guid("e51d7149-ca85-4739-bcf1-cedaa6928d68"), new Guid("63e29a28-bc90-4cd1-8f9e-ab9834bdde6c"), "AVATAR", "/wwwroot/Asserts/avatar.png" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContracts_EmployeeId",
                table: "EmployeeContracts",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMedias_EmployeeID",
                table: "EmployeeMedias",
                column: "EmployeeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeContracts");

            migrationBuilder.DropTable(
                name: "EmployeeMedias");

            migrationBuilder.DropTable(
                name: "Relatives");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
