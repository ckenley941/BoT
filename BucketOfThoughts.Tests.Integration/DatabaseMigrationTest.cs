using BucketOfThoughts.Services.Thoughts.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json;

namespace BucketOfThoughts.Tests.Integration
{
    public class DatabaseMigrationTest 
    {
        protected readonly ThoughtsDbContext dbContext;
        public DatabaseMigrationTest(ThoughtsDbContext dbContext) => this.dbContext = dbContext;

        public static readonly object[][] dbMigrationData =
        {
            new object[] { @"C:\Users\ckenl\Documents\", "ThoughtDetails" },
        };

        [Theory (Skip = "Batch process and not a test.")]
        //[Theory]
        [MemberData(nameof(dbMigrationData))]
        public async Task ExportTableData(string filePath, string tableName)
        {
            var dbRecords = await dbContext.ThoughtDetails.ToListAsync();
            await using FileStream createStream = File.Create($"{filePath}ThoughtDetails.json");
            await JsonSerializer.SerializeAsync(createStream, dbRecords);
        }
        
        [Theory (Skip = "Batch process and not a test.")]
        //[Theory]
        [InlineData(@"C:\Users\ckenl\Documents\")]
        public async Task ImportTableData(string filePath)
        {
            List<ThoughtDetail> importData = new List<ThoughtDetail>(); //For now this is hard code but eventually create system

            using (StreamReader r = new StreamReader($"{filePath}ThoughtDetails.json"))
            {
                string json = r.ReadToEnd();
                importData = JsonSerializer.Deserialize<List<ThoughtDetail>>(json);
            }
        }
    }
}