using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BucketOfThoughts.Services.Thoughts.Migrations
{
    /// <inheritdoc />
    public partial class AddColumn_ActivityDayOfWeek_OutdoorActivityLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActivityDayOfWeek",
                table: "OutdoorActivityLog",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivityDayOfWeek",
                table: "OutdoorActivityLog");
        }
    }
}
