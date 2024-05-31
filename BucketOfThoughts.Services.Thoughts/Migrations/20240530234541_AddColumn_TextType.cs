using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BucketOfThoughts.Services.Thoughts.Migrations
{
    /// <inheritdoc />
    public partial class AddColumn_TextType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TextType",
                table: "Thought",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValueSql: "'PlainText'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TextType",
                table: "Thought");
        }
    }
}
