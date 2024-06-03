using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BucketOfThoughts.Services.Thoughts.Migrations
{
    /// <inheritdoc />
    public partial class AddTable_OutdoorActivityLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OutdoorActivityLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "(getutcdate())"),
                    ModifiedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ActivityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ActivityDate = table.Column<DateTime>(type: "date", nullable: false),
                    GeographicArea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActivityLength = table.Column<double>(type: "float", nullable: true),
                    ElevationGain = table.Column<double>(type: "float", nullable: true),
                    ActivityTime = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutdoorActivityLog", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutdoorActivityLog");
        }
    }
}
