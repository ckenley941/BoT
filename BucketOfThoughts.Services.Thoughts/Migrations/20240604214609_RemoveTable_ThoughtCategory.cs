using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BucketOfThoughts.Services.Thoughts.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTable_ThoughtCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Thought_ThoughtCategory",
                table: "Thought");

            migrationBuilder.DropTable(
                name: "ThoughtCategory");

            migrationBuilder.DropIndex(
                name: "IX_Thought_ThoughtCategoryId",
                table: "Thought");

            migrationBuilder.DropColumn(
                name: "ThoughtCategoryId",
                table: "Thought");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThoughtCategoryId",
                table: "Thought",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ThoughtCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "(getutcdate())"),
                    ModifiedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ThoughtModuleId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThoughtCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThoughtCategory_ThoughtModule",
                        column: x => x.ThoughtModuleId,
                        principalTable: "ThoughtModule",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Thought_ThoughtCategoryId",
                table: "Thought",
                column: "ThoughtCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ThoughtCategory_ThoughtModuleId",
                table: "ThoughtCategory",
                column: "ThoughtModuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Thought_ThoughtCategory",
                table: "Thought",
                column: "ThoughtCategoryId",
                principalTable: "ThoughtCategory",
                principalColumn: "Id");
        }
    }
}
