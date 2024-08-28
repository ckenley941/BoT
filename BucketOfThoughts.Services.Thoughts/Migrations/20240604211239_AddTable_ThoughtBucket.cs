using System;
using BucketOfThoughts.Data;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BucketOfThoughts.Services.Thoughts.Migrations
{
    /// <inheritdoc />
    public partial class AddTable_ThoughtBucket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThoughtBucketId",
                table: "Thought",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ThoughtBucket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "(getutcdate())"),
                    ModifiedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ThoughtModuleId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: true),
                    ShowOnDashboard = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThoughtBucket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThoughtBucket_ThoughtModule",
                        column: x => x.ThoughtModuleId,
                        principalTable: "ThoughtModule",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Thought_ThoughtBucketId",
                table: "Thought",
                column: "ThoughtBucketId");

            migrationBuilder.CreateIndex(
                name: "IX_ThoughtBucket_ThoughtModuleId",
                table: "ThoughtBucket",
                column: "ThoughtModuleId");

            SeedData.MoveFromThoughtCategoryToThoughtBucket(migrationBuilder);

            migrationBuilder.AddForeignKey(
                name: "FK_Thought_ThoughtBucket",
                table: "Thought",
                column: "ThoughtBucketId",
                principalTable: "ThoughtBucket",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Thought_ThoughtBucket",
                table: "Thought");

            migrationBuilder.DropTable(
                name: "ThoughtBucket");

            migrationBuilder.DropIndex(
                name: "IX_Thought_ThoughtBucketId",
                table: "Thought");

            migrationBuilder.DropColumn(
                name: "ThoughtBucketId",
                table: "Thought");
        }
    }
}
