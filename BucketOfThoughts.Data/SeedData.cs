using Microsoft.EntityFrameworkCore.Migrations;

namespace BucketOfThoughts.Data
{
    public static class SeedData
    {
        public static void AddDefaultThoughtModule(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF NOT EXISTS (SELECT id FROM ThoughtModule WHERE Description = 'ThoughtModule')
                      BEGIN
                         INSERT INTO ThoughtModule (Description) VALUES ('Thought')
                         INSERT INTO ThoughtCategory (ModifiedDateTime, ThoughtModuleId, Description, SortOrder)
                         VALUES (GETUTCDATE(), SCOPE_IDENTITY(), 'Thought', 1)
                      END
                      GO");
        }

        public static void MoveFromThoughtCategoryToThoughtBucket(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                  INSERT INTO ThoughtBucket (ModifiedDateTime, ThoughtModuleId, Description, ParentId, SortOrder)
                  SELECT ModifiedDateTime, ThoughtModuleId, Description, ParentId, SortOrder FROM ThoughtCategory;
                  UPDATE THOUGHT SET ThoughtBucketId = ThoughtCategoryId");
        }
    }
}
