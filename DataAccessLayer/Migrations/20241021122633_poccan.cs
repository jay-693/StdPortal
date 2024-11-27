using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Migrations
{
    /// <inheritdoc />
    public partial class poccan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SemwiseAttendences",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<decimal>(
                name: "Average",
                table: "SemwiseAttendences",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Month",
                table: "SemwiseAttendences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SemwiseAttendences",
                table: "SemwiseAttendences",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SemwiseAttendences",
                table: "SemwiseAttendences");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SemwiseAttendences");

            migrationBuilder.DropColumn(
                name: "Average",
                table: "SemwiseAttendences");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "SemwiseAttendences");
        }
    }
}
