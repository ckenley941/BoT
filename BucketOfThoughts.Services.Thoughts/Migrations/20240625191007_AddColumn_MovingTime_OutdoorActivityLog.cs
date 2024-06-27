using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BucketOfThoughts.Services.Thoughts.Migrations
{
    /// <inheritdoc />
    public partial class AddColumn_MovingTime_OutdoorActivityLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActivityTime",
                table: "OutdoorActivityLog",
                newName: "TotalTime");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "MovingTime",
                table: "OutdoorActivityLog",
                type: "time",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovingTime",
                table: "OutdoorActivityLog");

            migrationBuilder.RenameColumn(
                name: "TotalTime",
                table: "OutdoorActivityLog",
                newName: "ActivityTime");
        }
    }
}
