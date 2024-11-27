using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Migrations
{
    /// <inheritdoc />
    public partial class MigrationName2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attendence",
                table: "SemwiseAttendences");

            migrationBuilder.DropColumn(
                name: "Average",
                table: "SemwiseAttendences");

            migrationBuilder.AddColumn<int>(
                name: "NoOfAbsents",
                table: "SemwiseAttendences",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoOfPresents",
                table: "SemwiseAttendences",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalWorkingDays",
                table: "SemwiseAttendences",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoOfAbsents",
                table: "SemwiseAttendences");

            migrationBuilder.DropColumn(
                name: "NoOfPresents",
                table: "SemwiseAttendences");

            migrationBuilder.DropColumn(
                name: "TotalWorkingDays",
                table: "SemwiseAttendences");

            migrationBuilder.AddColumn<decimal>(
                name: "Attendence",
                table: "SemwiseAttendences",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Average",
                table: "SemwiseAttendences",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
