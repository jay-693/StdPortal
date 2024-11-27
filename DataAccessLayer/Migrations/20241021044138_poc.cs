using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Migrations
{
    /// <inheritdoc />
    public partial class poc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Holidays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Date = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Occasion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holidays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SignUps",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastLoginTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignUps", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "BooksStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Sno = table.Column<int>(type: "int", nullable: false),
                    BookName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Penalty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooksStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BooksStatus_SignUps_StudentId",
                        column: x => x.StudentId,
                        principalTable: "SignUps",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BorrowedBooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Sno = table.Column<int>(type: "int", nullable: false),
                    BookName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BorrowedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowedBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BorrowedBooks_SignUps_StudentId",
                        column: x => x.StudentId,
                        principalTable: "SignUps",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamTimeTables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Sno = table.Column<int>(type: "int", nullable: false),
                    SubjectCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExamDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Timing = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Semester = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamTimeTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamTimeTables_SignUps_StudentId",
                        column: x => x.StudentId,
                        principalTable: "SignUps",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LabInternalMarks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Sno = table.Column<int>(type: "int", nullable: false),
                    SubjectCodeLabCode = table.Column<string>(name: "Subject Code/Lab Code", type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubjectNameLabName = table.Column<string>(name: "Subject Name/Lab Name", type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MidMarks30LabInternalMarks40 = table.Column<decimal>(name: "Mid Marks(30)/Lab InternalMarks(40)", type: "decimal(18,2)", nullable: false),
                    Semester = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabInternalMarks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabInternalMarks_SignUps_StudentId",
                        column: x => x.StudentId,
                        principalTable: "SignUps",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SemwiseAttendences",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Semester = table.Column<int>(type: "int", nullable: false),
                    Attendence = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_SemwiseAttendences_SignUps_StudentId",
                        column: x => x.StudentId,
                        principalTable: "SignUps",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SemwiseGradesDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Sno = table.Column<int>(type: "int", nullable: false),
                    SubjectcodeLabCode = table.Column<string>(name: "Subject code/Lab Code", type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubjectNameLabName = table.Column<string>(name: "Subject Name/Lab Name", type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Monthyear = table.Column<string>(name: "Month&year", type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Grade = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Semester = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemwiseGradesDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SemwiseGradesDetails_SignUps_StudentId",
                        column: x => x.StudentId,
                        principalTable: "SignUps",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Supply",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Sno = table.Column<int>(type: "int", nullable: false),
                    SubjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Semester = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supply", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Supply_SignUps_StudentId",
                        column: x => x.StudentId,
                        principalTable: "SignUps",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BooksStatus_StudentId",
                table: "BooksStatus",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedBooks_StudentId",
                table: "BorrowedBooks",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamTimeTables_StudentId",
                table: "ExamTimeTables",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_LabInternalMarks_StudentId",
                table: "LabInternalMarks",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_SemwiseAttendences_StudentId",
                table: "SemwiseAttendences",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_SemwiseGradesDetails_StudentId",
                table: "SemwiseGradesDetails",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Supply_StudentId",
                table: "Supply",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BooksStatus");

            migrationBuilder.DropTable(
                name: "BorrowedBooks");

            migrationBuilder.DropTable(
                name: "ExamTimeTables");

            migrationBuilder.DropTable(
                name: "Holidays");

            migrationBuilder.DropTable(
                name: "LabInternalMarks");

            migrationBuilder.DropTable(
                name: "SemwiseAttendences");

            migrationBuilder.DropTable(
                name: "SemwiseGradesDetails");

            migrationBuilder.DropTable(
                name: "Supply");

            migrationBuilder.DropTable(
                name: "SignUps");
        }
    }
}
