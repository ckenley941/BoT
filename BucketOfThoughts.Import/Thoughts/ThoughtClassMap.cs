using Common.Data.Objects.Thoughts;
using CsvHelper.Configuration;

namespace BucketOfThoughts.Imports.Thoughts
{
    public class ThoughtClassMap : ClassMap<ThoughtVm>
    {
        public ThoughtClassMap()
        {
            Map(x => x.Thought)
                .Name("Description");

            Map(x => x.Category)
                .Name("Category");

            Map(x => x.Detail)
                .Name("Detail");

            Map(x => x.Source)
                .Name("Source");
        }
    }
}