using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCriteria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EvaluationCriterias",
                columns: table => new
                {
                    CriterionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<double>(type: "float", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationCriterias", x => x.CriterionID);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationPeriods",
                columns: table => new
                {
                    PeriodID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationPeriods", x => x.PeriodID);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeEvaluations",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EvaluatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EvaluationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalScore = table.Column<double>(type: "float", nullable: false),
                    DetailJson = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeEvaluations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EmployeeEvaluations_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeEvaluations_EvaluationPeriods_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "EvaluationPeriods",
                        principalColumn: "PeriodID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EvaluationCriterias",
                columns: new[] { "CriterionID", "Category", "Description", "Name", "Weight" },
                values: new object[,]
                {
                    { new Guid("2a63d8a0-c090-4b9a-a95c-2390995b2527"), "Performance", "The quality and accuracy of work produced", "Quality of Work", 0.20000000000000001 },
                    { new Guid("7fbad584-5c72-4e12-a35f-19c80ba0c143"), "Skills", "Assessment of employee's technical knowledge and skills relevant to their position", "Technical Skills", 0.25 },
                    { new Guid("8fa78c5e-b701-45dd-b721-40c76c32d6a2"), "Soft Skills", "Ability to work cooperatively with others and contribute to team objectives", "Teamwork", 0.10000000000000001 },
                    { new Guid("aecfd0a1-e9b3-40e3-b7b7-683d42c7a654"), "Discipline", "Consistency in arriving on time and maintaining good attendance", "Punctuality & Attendance", 0.050000000000000003 },
                    { new Guid("bd5d538c-ac5a-401d-a65b-f1d429590025"), "Soft Skills", "Willingness to take on responsibilities and challenges", "Initiative", 0.050000000000000003 },
                    { new Guid("d7a64787-b9a0-4f41-a4fb-cffe9d0e19fc"), "Soft Skills", "Ability to communicate effectively with team members and stakeholders", "Communication", 0.14999999999999999 },
                    { new Guid("e6f1ca8e-1d3d-4c1a-9239-b1ca765b92b3"), "Skills", "Ability to analyze situations and develop effective solutions", "Problem Solving", 0.20000000000000001 }
                });

            migrationBuilder.InsertData(
                table: "EvaluationPeriods",
                columns: new[] { "PeriodID", "EndDate", "Name", "StartDate" },
                values: new object[,]
                {
                    { new Guid("3b1ca0bf-5fc2-4e2a-b447-eba1ba7ab4f6"), new DateTime(2023, 12, 31, 23, 59, 59, 0, DateTimeKind.Unspecified), "2023 Annual Review", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("4f71bf52-868d-43f5-8a56-26b3b0906833"), new DateTime(2023, 3, 31, 23, 59, 59, 0, DateTimeKind.Unspecified), "Q1 2023 Performance Review", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a0d94e5c-931e-4f40-9d7d-9924e9a620a9"), new DateTime(2023, 12, 31, 23, 59, 59, 0, DateTimeKind.Unspecified), "Q4 2023 Performance Review", new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d5bbe570-ed44-4bc0-9a53-92face1e8c19"), new DateTime(2023, 6, 30, 23, 59, 59, 0, DateTimeKind.Unspecified), "Q2 2023 Performance Review", new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f74d6c5c-5fd9-4ef1-ae7e-52e3c50d3d3a"), new DateTime(2023, 9, 30, 23, 59, 59, 0, DateTimeKind.Unspecified), "Q3 2023 Performance Review", new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "EmployeeEvaluations",
                columns: new[] { "ID", "DetailJson", "EmployeeID", "EvaluationDate", "EvaluatorId", "PeriodId", "TotalScore" },
                values: new object[,]
                {
                    { new Guid("a4e3f08f-b6df-4d58-8377-8b97fc5cf66e"), "{\"7FBAD584-5C72-4E12-A35F-19C80BA0C143\":4.5,\"D7A64787-B9A0-4F41-A4FB-CFFE9D0E19FC\":4.0,\"E6F1CA8E-1D3D-4C1A-9239-B1CA765B92B3\":4.3,\"2A63D8A0-C090-4B9A-A95C-2390995B2527\":4.1,\"8FA78C5E-B701-45DD-B721-40C76C32D6A2\":4.2,\"AECFD0A1-E9B3-40E3-B7B7-683D42C7A654\":3.8,\"BD5D538C-AC5A-401D-A65B-F1D429590025\":4.0}", new Guid("63e29a28-bc90-4cd1-8f9e-ab9834bdde6c"), new DateTime(2023, 4, 5, 10, 0, 0, 0, DateTimeKind.Unspecified), new Guid("02cbe3e9-f99b-44d0-8f95-30b13d5f6f5d"), new Guid("4f71bf52-868d-43f5-8a56-26b3b0906833"), 4.2000000000000002 },
                    { new Guid("cca92ac7-7e25-4a8c-8f62-db3521909f07"), "{\"7FBAD584-5C72-4E12-A35F-19C80BA0C143\":3.7,\"D7A64787-B9A0-4F41-A4FB-CFFE9D0E19FC\":4.1,\"E6F1CA8E-1D3D-4C1A-9239-B1CA765B92B3\":3.6,\"2A63D8A0-C090-4B9A-A95C-2390995B2527\":3.8,\"8FA78C5E-B701-45DD-B721-40C76C32D6A2\":4.2,\"AECFD0A1-E9B3-40E3-B7B7-683D42C7A654\":3.5,\"BD5D538C-AC5A-401D-A65B-F1D429590025\":3.7}", new Guid("d05780d4-5742-40ca-8403-0febd44b1555"), new DateTime(2023, 4, 6, 14, 30, 0, 0, DateTimeKind.Unspecified), new Guid("02cbe3e9-f99b-44d0-8f95-30b13d5f6f5d"), new Guid("4f71bf52-868d-43f5-8a56-26b3b0906833"), 3.7999999999999998 },
                    { new Guid("d6b2d82a-1f74-4b5a-bf7e-638e5c6e366a"), "{\"7FBAD584-5C72-4E12-A35F-19C80BA0C143\":4.6,\"D7A64787-B9A0-4F41-A4FB-CFFE9D0E19FC\":4.3,\"E6F1CA8E-1D3D-4C1A-9239-B1CA765B92B3\":4.7,\"2A63D8A0-C090-4B9A-A95C-2390995B2527\":4.5,\"8FA78C5E-B701-45DD-B721-40C76C32D6A2\":4.4,\"AECFD0A1-E9B3-40E3-B7B7-683D42C7A654\":4.2,\"BD5D538C-AC5A-401D-A65B-F1D429590025\":4.8}", new Guid("63e29a28-bc90-4cd1-8f9e-ab9834bdde6c"), new DateTime(2023, 7, 10, 9, 15, 0, 0, DateTimeKind.Unspecified), new Guid("02cbe3e9-f99b-44d0-8f95-30b13d5f6f5d"), new Guid("d5bbe570-ed44-4bc0-9a53-92face1e8c19"), 4.5 },
                    { new Guid("e5a9b9d3-04a5-4a2b-9e38-78e7b250b4df"), "{\"7FBAD584-5C72-4E12-A35F-19C80BA0C143\":3.9,\"D7A64787-B9A0-4F41-A4FB-CFFE9D0E19FC\":4.2,\"E6F1CA8E-1D3D-4C1A-9239-B1CA765B92B3\":3.8,\"2A63D8A0-C090-4B9A-A95C-2390995B2527\":4.1,\"8FA78C5E-B701-45DD-B721-40C76C32D6A2\":4.3,\"AECFD0A1-E9B3-40E3-B7B7-683D42C7A654\":3.7,\"BD5D538C-AC5A-401D-A65B-F1D429590025\":4.0}", new Guid("63e29a28-bc90-4cd1-8f9e-ab9834bdde6c"), new DateTime(2023, 7, 11, 11, 0, 0, 0, DateTimeKind.Unspecified), new Guid("02cbe3e9-f99b-44d0-8f95-30b13d5f6f5d"), new Guid("d5bbe570-ed44-4bc0-9a53-92face1e8c19"), 4.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEvaluations_EmployeeID",
                table: "EmployeeEvaluations",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEvaluations_PeriodId",
                table: "EmployeeEvaluations",
                column: "PeriodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeEvaluations");

            migrationBuilder.DropTable(
                name: "EvaluationCriterias");

            migrationBuilder.DropTable(
                name: "EvaluationPeriods");
        }
    }
}
