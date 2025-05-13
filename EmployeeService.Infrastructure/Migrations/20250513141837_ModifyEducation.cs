using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyEducation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    EducationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Degree = table.Column<int>(type: "int", nullable: true),
                    Major = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    School = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.EducationID);
                    table.ForeignKey(
                        name: "FK_Educations_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Educations",
                columns: new[] { "EducationID", "Degree", "Description", "EmployeeID", "EndDate", "Major", "School", "StartDate" },
                values: new object[,]
                {
                    { new Guid("087694ac-137d-443f-8ce4-5e6ad5bda70b"), 2, "Chuyên ngành kế toán doanh nghiệp", new Guid("d05780d4-5742-40ca-8403-0febd44b1555"), new DateTime(2018, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kế toán", "Đại học Kinh tế Quốc dân", new DateTime(2014, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("2f954c2f-6629-4faf-8f20-bb9a60be7c1a"), 3, "Tốt nghiệp loại giỏi", new Guid("d05780d4-5742-40ca-8403-0febd44b1555"), new DateTime(2021, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tài chính", "Học viện Tài chính", new DateTime(2019, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("672bbf18-7e34-43b4-812b-9299b2b23ddc"), 3, "Chuyên ngành phần mềm", new Guid("63e29a28-bc90-4cd1-8f9e-ab9834bdde6c"), new DateTime(2019, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Công nghệ thông tin", "Đại học Bách Khoa Hà Nội", new DateTime(2015, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("87c19085-f180-439a-8c79-92976f9c52d6"), 3, "Nghiên cứu máy học nâng cao", new Guid("63e29a28-bc90-4cd1-8f9e-ab9834bdde6c"), new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Trí tuệ nhân tạo", "Đại học Công nghệ - ĐHQGHN", new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Educations_EmployeeID",
                table: "Educations",
                column: "EmployeeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Educations");
        }
    }
}
