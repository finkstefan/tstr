using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamRegistrationService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExamRegistrations",
                columns: table => new
                {
                    ExamRegistrationId = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    StudentIndex = table.Column<string>(nullable: true),
                    StudentFirstName = table.Column<string>(nullable: true),
                    StudentLastName = table.Column<string>(nullable: true),
                    StudentEnrolledYear = table.Column<int>(nullable: false),
                    StudentCurrentSemester = table.Column<int>(nullable: false),
                    SubjectId = table.Column<Guid>(nullable: false),
                    SubjectCode = table.Column<string>(nullable: true),
                    SubjectName = table.Column<string>(nullable: true),
                    SubjectTerm = table.Column<int>(nullable: false),
                    SubjectSemester = table.Column<int>(nullable: false),
                    SubjectExamTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamRegistrations", x => x.ExamRegistrationId);
                });

            migrationBuilder.InsertData(
                table: "ExamRegistrations",
                columns: new[] { "ExamRegistrationId", "StudentCurrentSemester", "StudentEnrolledYear", "StudentFirstName", "StudentId", "StudentIndex", "StudentLastName", "SubjectCode", "SubjectExamTime", "SubjectId", "SubjectName", "SubjectSemester", "SubjectTerm" },
                values: new object[] { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), 1, 1, "Petar", new Guid("044f3de0-a9dd-4c2e-b745-89976a1b2a36"), "IT1/2020", "Petrović", "S12020", new DateTime(2020, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new Guid("21ad52f8-0281-4241-98b0-481566d25e4f"), "Subject 1", 1, 1 });

            migrationBuilder.InsertData(
                table: "ExamRegistrations",
                columns: new[] { "ExamRegistrationId", "StudentCurrentSemester", "StudentEnrolledYear", "StudentFirstName", "StudentId", "StudentIndex", "StudentLastName", "SubjectCode", "SubjectExamTime", "SubjectId", "SubjectName", "SubjectSemester", "SubjectTerm" },
                values: new object[] { new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), 1, 2, "Marko", new Guid("32cd906d-8bab-457c-ade2-fbc4ba523029"), "IT2/2019", "Marković", "S22020", new DateTime(2020, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9d8bab08-f442-4297-8ab5-ddfe08e335f3"), "Subject 2", 2, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamRegistrations");
        }
    }
}
