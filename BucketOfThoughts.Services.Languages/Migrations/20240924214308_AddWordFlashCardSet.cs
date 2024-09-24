using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BucketOfThoughts.Services.Languages.Migrations
{
    /// <inheritdoc />
    public partial class AddWordFlashCardSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WordFlashCardSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "(getutcdate())"),
                    ModifiedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordFlashCardSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WordFlashCardSetDetail",
                columns: table => new
                {
                    WordFlashCardSetId = table.Column<int>(type: "int", nullable: false),
                    WordXrefId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordFlashCardSetDetail", x => new { x.WordXrefId, x.WordFlashCardSetId });
                    table.ForeignKey(
                        name: "FK_WordFlashCardSetDetail_WordFlashCardSet",
                        column: x => x.WordFlashCardSetId,
                        principalTable: "WordFlashCardSet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WordFlashCardSetDetail_WordXref",
                        column: x => x.WordXrefId,
                        principalTable: "WordXref",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WordFlashCardSetDetail_WordFlashCardSetId",
                table: "WordFlashCardSetDetail",
                column: "WordFlashCardSetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WordFlashCardSetDetail");

            migrationBuilder.DropTable(
                name: "WordFlashCardSet");
        }
    }
}
