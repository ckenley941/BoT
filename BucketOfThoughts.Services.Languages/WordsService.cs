using BucketOfThoughts.Services.Languages.Data;
using BucketOfThoughts.Services.Languages.Objects;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BucketOfThoughts.Services.Languages
{
    public class WordsService 
    {
        private readonly LanguageDbContext _dbContext;
        public WordsService(LanguageDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> InsertOrUpdateWordCardAsync(InsertWordCardDto insertedWord)
        {
            var word = await InsertWordIfNotExistsAsync(insertedWord.PrimaryWord, 2);
            if (insertedWord.Pronunication.Any())
            {
                await InsertPronunicationIfNotExistsAsync(word.WordId, insertedWord.Pronunication);
            }

            foreach (var wordDict in insertedWord.WordDictionary)
            {
                var word2 = await InsertWordIfNotExistsAsync(wordDict.Word, 1);

                var xref = await _dbContext.WordXrefs.FirstOrDefaultAsync(x => x.WordId1 == word.WordId && x.WordId2 == word2.WordId);
                if (xref == null)
                {
                    xref = new WordXref()
                    {
                        WordId1 = word.WordId,
                        WordId2 = word2.WordId,
                        IsPrimaryTranslation = wordDict.IsPrimaryTranslation,
                        SortOrder = wordDict.SortOrder
                    };

                    await _dbContext.WordXrefs.AddAsync(xref);
                    await _dbContext.SaveChangesAsync();
                }

                foreach (var wc in wordDict.WordContexts)
                {
                    await InsertContextIfNotExistsAsync(wc, xref.WordXrefId);
                }

                //Come back to word relationships
                //if (wordDict.WordRelationship != null)
                //{
                //    var wordRelationship = await _dbContext.WordRelationships.FirstOrDefaultAsync(x => x.WordId1 == xref.WordId1 && x.WordId2 == xref.WordId2);
                //    if (wordRelationship == null)
                //    {
                //        //Check to make sure only one flag is checked
                //        await _dbContext.WordRelationships.AddAsync(new WordRelationship()
                //        {
                //            IsRelated = wordDict.WordRelationship.IsRelated,
                //            IsPhrase = wordDict.WordRelationship.IsPhrase,
                //            IsSynonym = wordDict.WordRelationship.IsSynonym,
                //            IsAntonym = wordDict.WordRelationship.IsAntonym,
                //        });
                //        await _dbContext.SaveChangesAsync();
                //    }
                //}
            }

            return word.WordId;
        }

        public async Task<Word> InsertWordIfNotExistsAsync(string primaryWord, int languageId)
        {
            var word = await _dbContext.Words.FirstOrDefaultAsync(x => x.Description == primaryWord);
            if (word == null)
            {
                word = new Word()
                {
                    Description = primaryWord,
                    LanguageId = languageId
                };

                await _dbContext.Words.AddAsync(word);
                await _dbContext.SaveChangesAsync();
            }
            return word;
        }

        public async Task<bool> InsertPronunicationIfNotExistsAsync(int wordId, List<InsertWordPronunciation> pronunciation)
        {
            bool success = true;

            if (!_dbContext.WordPronunciations.Any(x => x.WordId == wordId))
            {
                foreach (var p in pronunciation)
                {
                    _dbContext.WordPronunciations.Add(new WordPronunciation()
                    {
                        WordId = wordId,
                        Phonetic = p.Phonetic
                    });
                }
                success &= await _dbContext.SaveChangesAsync() > 0;
            }
            return success;
        }

        private async Task<bool> InsertContextIfNotExistsAsync(InsertWordContextDto wc, int wordXrefId)
        {
            bool success = true;

            if (!string.IsNullOrEmpty(wc.ContextDesc))
            {
                var wordContext = await _dbContext.WordContexts.FirstOrDefaultAsync(x => x.ContextDesc == wc.ContextDesc && x.WordXrefId == wordXrefId);
                if (wordContext == null)
                {
                    wordContext = new WordContext()
                    {
                        ContextDesc = wc.ContextDesc,
                        WordXrefId = wordXrefId,
                        SortOrder = wc.SortOrder
                    };

                    await _dbContext.WordContexts.AddAsync(wordContext);
                    success &= await _dbContext.SaveChangesAsync() > 0;
                }

                if (wc.Examples.Any())
                {
                    var existingExamples = _dbContext.WordExamples.Where(x => x.WordContextId == wordContext.WordContextId).AsEnumerable();
                    foreach (var example in wc.Examples)
                    {
                        if (!existingExamples.Any(x => x.Translation1 == example.Translation1 && x.Translation2 == example.Translation2))
                        {
                            await _dbContext.WordExamples.AddAsync(new WordExample()
                            {
                                Translation1 = example.Translation1,
                                Translation2 = example.Translation2,
                                WordContextId = wordContext.WordContextId
                            });
                        }
                    }
                    success &= await _dbContext.SaveChangesAsync() > 0;
                }
            }

            return success;
        }

        public async Task<IEnumerable<GetWordDto>> GetAsync()
        {
            var spanishWords = _dbContext.Words.Where(x => x.LanguageId == 2)
                .Join(_dbContext.WordXrefs.Where(x => x.IsPrimaryTranslation == true), w => w.WordId, wx => wx.WordId1, (w, wx) => new { Word = w, Xref = wx })
                .Select(x => new GetWordDto()
                {
                    Word1Id = x.Word.WordId,
                    Word1 = x.Word.Description,
                    Word2Id = x.Xref.WordId2,
                    Word2 = x.Xref.WordId2Navigation.Description,
                    Word1Example = x.Xref.WordContexts.OrderBy(x => x.SortOrder).FirstOrDefault().WordExamples.FirstOrDefault().Translation1,
                    Word2Example = x.Xref.WordContexts.OrderBy(x => x.SortOrder).FirstOrDefault().WordExamples.FirstOrDefault().Translation2,
                });

            return spanishWords;
        }

        public async Task<WordTranslationDto> GetByIdAsync(int id)
        {
            var word =
               await _dbContext.Words
               .Where(x => x.WordId == id)
               .Select(x => new WordTranslationDto(x.WordId, x.WordGuid, x.Description))
               .FirstOrDefaultAsync();

            if (word == null)
            {
                throw new Exception("Record not found"); //TODO make custom not found exception
            }

            word.PrimaryTranslation = await GetPrimaryTranslation(word.Id);
            word.Pronunication = await _dbContext.WordPronunciations.Where(x => x.WordId == word.Id).Select(x => x.Phonetic).ToListAsync();
            return word;
        }

        public async Task<WordTranslationDto> GetByWord(string description)
        {
            var word =
               await _dbContext.Words
               .Where(x => x.Description == description)
               .Select(x => new WordTranslationDto(x.WordId, x.WordGuid, x.Description))
               .FirstOrDefaultAsync();

            if (word == null)
            {
                throw new Exception("Record not found"); //TODO make custom not found exception
            }

            word.PrimaryTranslation = await GetPrimaryTranslation(word.Id);

            return word;
        }

        public async Task<WordTranslationDto> GetRandomWordAsync()
        {
            var rnd = new Random();

            //For now just doing Spanish words (LanguageId = 2)
            var spanishWords = _dbContext.Words.Where(x => x.LanguageId == 2).ToList();
            if (!spanishWords.Any())
            {
                throw new Exception("Words not found"); //TODO make custom not found exception
            }

            var randomWord = spanishWords[rnd.Next(spanishWords.Count())];
            var word = new WordTranslationDto(randomWord.WordId, randomWord.WordGuid, randomWord.Description);
            word.PrimaryTranslation = await GetPrimaryTranslation(word.Id);

            word.Pronunication = await _dbContext.WordPronunciations.Where(x => x.WordId == word.Id).Select(x => x.Phonetic).ToListAsync();

            return word;
        }

        public async Task<List<WordDto>> GetTranslationsAsync(int id)
        {
            var wordTranslations =
               await JoinXrefSelectWord2(x => x.WordId1 == id)
               .Select(ew => new WordDto(ew.WordId, ew.WordGuid, ew.Description))
               .ToListAsync();

            return wordTranslations;
        }

        public async Task<List<WordContextDto>> GetTranslationsWithContextAsync(int id)
        {
            var wordContexts = await
                _dbContext.WordXrefs.Where(x => x.WordId1 == id)
                .Join(_dbContext.WordContexts, xref => xref.WordXrefId, wc => wc.WordXrefId, (xref, wc) => new { xref.WordId2, Context = wc })
                .Join(_dbContext.Words, wc => wc.WordId2, w => w.WordId, (wc, w) => new
                {
                    wc.Context,
                    Word = new WordDto()
                    {
                        Id = w.WordId,
                        Guid = w.WordGuid,
                        Word = w.Description,
                        Examples = wc.Context.WordExamples.Select(x =>
                        new WordExampleDto(x.WordExampleId, x.Translation1, x.Translation2)).ToList()
                    }
                })
                .GroupBy(x => x.Context.ContextDesc)
                .Select(x => new WordContextDto()
                {
                    ContextDesc = x.Key,
                    SortOrder = x.Max(y => y.Context.SortOrder),
                    Words = x.Select(y => y.Word).ToList(),
                })
                .OrderBy(x => x.SortOrder)
                .ToListAsync();

            return wordContexts;
        }

        public async Task<List<WordRelationshipDto>> GetWordRelationshipsAsync(int id)
        {
            var words =
                await _dbContext.WordRelationships.Where(x => x.WordId1 == id)
                .Join(_dbContext.Words, wr => wr.WordId2, w => w.WordId, (wr, w) => new { WordRelationship = wr, Word = w })
                .Union
                (
                    _dbContext.WordRelationships.Where(x => x.WordId2 == id)
                    .Join(_dbContext.Words, wr => wr.WordId1, w => w.WordId, (wr, w) => new { WordRelationship = wr, Word = w })
                )
                .Select(w => new WordRelationshipDto
                {
                    Id = w.Word.WordId,
                    Guid = w.Word.WordGuid,
                    Word = w.Word.Description,
                    IsRelated = w.WordRelationship.IsRelated,
                    IsPhrase = w.WordRelationship.IsPhrase,
                    IsSynonym = w.WordRelationship.IsSynonym,
                    IsAntonym = w.WordRelationship.IsAntonym,
                })
                .ToListAsync();

            var wordIds = words.Select(x => x.Id).ToList();

            var wordXrefs = _dbContext.WordXrefs.Where(x => wordIds.Contains(x.WordId1) && x.IsPrimaryTranslation == true)
                .Join(_dbContext.Words, xref => xref.WordId2, w => w.WordId, (xref, w) => new
                {
                    WordId = xref.WordId1,
                    TranslationWord = w
                })
                .Union(
                    _dbContext.WordXrefs.Where(x => wordIds.Contains(x.WordId2) && x.IsPrimaryTranslation == true)
                    .Join(_dbContext.Words, xref => xref.WordId1, w => w.WordId, (xref, w) => new
                    {
                        WordId = xref.WordId2,
                        TranslationWord = w
                    })
                ).ToList();

            foreach (var word in words)
            {
                var primaryTranslation = wordXrefs.FirstOrDefault(x => x.WordId == word.Id);
                if (primaryTranslation != null)
                {
                    word.PrimaryTranslation = new WordDto(primaryTranslation.TranslationWord.WordId, primaryTranslation.TranslationWord.WordGuid, primaryTranslation.TranslationWord.Description);
                }
            }

            return words;
        }

        private async Task<WordDto> GetPrimaryTranslation(int wordId)
        {
            return await JoinXrefSelectWord2(x => x.WordId1 == wordId)
               .Select(x => new WordDto(x.WordId, x.WordGuid, x.Description))
               .FirstOrDefaultAsync() ?? new WordDto();
        }

        private IQueryable<Word> JoinXrefSelectWord2(Expression<Func<WordXref, bool>>? filter = null)
        {
            var wordXrefs = _dbContext.WordXrefs.AsQueryable();
            if (filter != null)
            {
                wordXrefs = wordXrefs.Where(filter);
            }
            return wordXrefs
                .Join(_dbContext.Words, xref => xref.WordId2, w => w.WordId, (xref, w) => new { Word = w, xref.SortOrder })
                .OrderBy(x => x.SortOrder)
                .Select(x => x.Word);
        }
    }
}
