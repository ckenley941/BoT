using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BucketOfThoughts.Services.Thoughts.Migrations
{
    /// <inheritdoc />
    public partial class AddThoughtsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ThoughtModule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "(getutcdate())"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThoughtModule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebsiteLink",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "(getutcdate())"),
                    ModifiedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    WebsiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebsiteDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebsiteLink", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Thought",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "(getutcdate())"),
                    ModifiedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ThoughtGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThoughtCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thought", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Thought_ThoughtCategory",
                        column: x => x.ThoughtCategoryId,
                        principalTable: "ThoughtCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RelatedThought",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "(getutcdate())"),
                    ThoughtId1 = table.Column<int>(type: "int", nullable: false),
                    ThoughtId2 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedThought", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelatedThought_Thought1",
                        column: x => x.ThoughtId1,
                        principalTable: "Thought",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RelatedThought_Thought2",
                        column: x => x.ThoughtId2,
                        principalTable: "Thought",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ThoughtDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "(getutcdate())"),
                    ModifiedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThoughtId = table.Column<int>(type: "int", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThoughtDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThoughtDetail_Thought",
                        column: x => x.ThoughtId,
                        principalTable: "Thought",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ThoughtWebsiteLink",
                columns: table => new
                {
                    ThoughtId = table.Column<int>(type: "int", nullable: false),
                    WebsiteLinkId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThoughtWebsiteLink", x => new { x.ThoughtId, x.WebsiteLinkId });
                    table.ForeignKey(
                        name: "FK_ThoughtWebsiteLink_Thought",
                        column: x => x.ThoughtId,
                        principalTable: "Thought",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ThoughtWebsiteLink_WebsiteLink",
                        column: x => x.WebsiteLinkId,
                        principalTable: "WebsiteLink",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RelatedThought_ThoughtId1",
                table: "RelatedThought",
                column: "ThoughtId1");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedThought_ThoughtId2",
                table: "RelatedThought",
                column: "ThoughtId2");

            migrationBuilder.CreateIndex(
                name: "IX_Thought_ThoughtCategoryId",
                table: "Thought",
                column: "ThoughtCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ThoughtCategory_ThoughtModuleId",
                table: "ThoughtCategory",
                column: "ThoughtModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_ThoughtDetail_ThoughtId",
                table: "ThoughtDetail",
                column: "ThoughtId");

            migrationBuilder.CreateIndex(
                name: "IX_ThoughtWebsiteLink_WebsiteLinkId",
                table: "ThoughtWebsiteLink",
                column: "WebsiteLinkId");

            SeedData(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RelatedThought");

            migrationBuilder.DropTable(
                name: "ThoughtDetail");

            migrationBuilder.DropTable(
                name: "ThoughtWebsiteLink");

            migrationBuilder.DropTable(
                name: "Thought");

            migrationBuilder.DropTable(
                name: "WebsiteLink");

            migrationBuilder.DropTable(
                name: "ThoughtCategory");

            migrationBuilder.DropTable(
                name: "ThoughtModule");
        }

        private void SeedData(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF NOT EXISTS (SELECT id FROM ThoughtModule WHERE Description = 'ThoughtModule')
                      BEGIN
                         INSERT INTO ThoughtModule (Description) VALUES ('Thought')
                         INSERT INTO ThoughtCategory (ModifiedDateTime, ThoughtModuleId, Description, SortOrder)
                         VALUES (GETUTCDATE(), SCOPE_IDENTITY(), 'Thought', 1)
                      END
                      GO");
        }
    }
}
