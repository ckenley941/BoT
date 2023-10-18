using Common.Data.Objects.Words;
using BucketOfThoughts.Services;
using EnsenaMe.Data.Contexts;
using EnsenaMe.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BucketOfThoughts.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordRelationshipsController : ControllerBase
    {
        private readonly IWordsService _wordsService;
        private readonly EnsenaMeContext _dbContext;

        public WordRelationshipsController(IWordsService wordsService, EnsenaMeContext dbContext)
        {
            _wordsService = wordsService;
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Post(string description, List<InsertWordPronunciation> pronunciation)
        {
            Word word = await _dbContext.Words.FirstOrDefaultAsync(x => x.Description == description);
            if (word == null)
            {
                word = await _wordsService.AddWordIfNotExistsAsync(description, (int)EnsenaMe.Data.Enums.Lanugage.Spanish);
            }
            bool success = await _wordsService.AddPronunicationIfNotExistsAsync(word.WordId, pronunciation);
            return success;
        }
    }
}
