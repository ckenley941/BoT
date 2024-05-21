using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BucketOfThoughts.Services.Languages.Migrations
{
    /// <inheritdoc />
    public partial class AddLanguageTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Word",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "(getutcdate())"),
                    ModifiedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Word", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WordPronunciation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "(getutcdate())"),
                    WordId = table.Column<int>(type: "int", nullable: false),
                    Phonetic = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordPronunciation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordPronunciation_Word",
                        column: x => x.WordId,
                        principalTable: "Word",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WordRelationship",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "(getutcdate())"),
                    ModifiedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    WordId1 = table.Column<int>(type: "int", nullable: false),
                    WordId2 = table.Column<int>(type: "int", nullable: false),
                    IsRelated = table.Column<bool>(type: "bit", nullable: false),
                    IsPhrase = table.Column<bool>(type: "bit", nullable: false),
                    IsSynonym = table.Column<bool>(type: "bit", nullable: false),
                    IsAntonym = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordRelationship", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordRelationship_Word1",
                        column: x => x.WordId1,
                        principalTable: "Word",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WordRelationship_Word2",
                        column: x => x.WordId2,
                        principalTable: "Word",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WordXref",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "(getutcdate())"),
                    WordId1 = table.Column<int>(type: "int", nullable: false),
                    WordId2 = table.Column<int>(type: "int", nullable: false),
                    IsPrimaryTranslation = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordXref", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordXref_Word1",
                        column: x => x.WordId1,
                        principalTable: "Word",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WordXref_Word2",
                        column: x => x.WordId2,
                        principalTable: "Word",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WordContext",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "(getutcdate())"),
                    ModifiedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    WordXrefId = table.Column<int>(type: "int", nullable: false),
                    ContextDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordContext", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordContext_WordXref",
                        column: x => x.WordXrefId,
                        principalTable: "WordXref",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WordExample",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "(getutcdate())"),
                    ModifiedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Translation1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Translation2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WordContextId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordExample", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordExample_WordContext",
                        column: x => x.WordContextId,
                        principalTable: "WordContext",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IDX_Language",
                table: "Word",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_WordContext_WordXrefId",
                table: "WordContext",
                column: "WordXrefId");

            migrationBuilder.CreateIndex(
                name: "IX_WordExample_WordContextId",
                table: "WordExample",
                column: "WordContextId");

            migrationBuilder.CreateIndex(
                name: "IX_WordPronunciation_WordId",
                table: "WordPronunciation",
                column: "WordId");

            migrationBuilder.CreateIndex(
                name: "IX_WordRelationship_WordId1",
                table: "WordRelationship",
                column: "WordId1");

            migrationBuilder.CreateIndex(
                name: "IX_WordRelationship_WordId2",
                table: "WordRelationship",
                column: "WordId2");

            migrationBuilder.CreateIndex(
                name: "IX_WordXref_WordId1",
                table: "WordXref",
                column: "WordId1");

            migrationBuilder.CreateIndex(
                name: "IX_WordXref_WordId2",
                table: "WordXref",
                column: "WordId2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WordExample");

            migrationBuilder.DropTable(
                name: "WordPronunciation");

            migrationBuilder.DropTable(
                name: "WordRelationship");

            migrationBuilder.DropTable(
                name: "WordContext");

            migrationBuilder.DropTable(
                name: "WordXref");

            migrationBuilder.DropTable(
                name: "Word");
        }
    }
}
