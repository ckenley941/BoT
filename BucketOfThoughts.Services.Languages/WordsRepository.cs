using BucketOfThoughts.Core.Infrastructure.BaseClasses;
using BucketOfThoughts.Services.Languages.Data;

namespace BucketOfThoughts.Services.Languages
{
    public class WordsRepository : BaseCrudRepositoryEF<Word>
    {
        public WordsRepository(LanguageDbContext context) : base(context) { }
    }
}
