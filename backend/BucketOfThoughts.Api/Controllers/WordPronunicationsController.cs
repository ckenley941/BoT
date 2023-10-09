using Common.Data.Objects.Words;
using BucketOfThoughts.Api.Services;
using EnsenaMe.Data.Contexts;
using EnsenaMe.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BucketOfThoughts.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordPronunicationsController : ControllerBase
    {
        private readonly IWordsService _wordsService;
        private readonly EnsenaMeContext _dbContext;

        public WordPronunicationsController(IWordsService wordsService, EnsenaMeContext dbContext)
        {
            _wordsService = wordsService;
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Post(string description, List<InsertWordPronunciation> pronunciation)
        {
            Word word = await _wordsService.AddWordIfNotExistsAsync(description, (int)EnsenaMe.Data.Enums.Lanugage.Spanish);
            bool success = word.WordId > 0;
            if (success)
            {
                success = await _wordsService.AddPronunicationIfNotExistsAsync(word.WordId, pronunciation);
            }
            return success;
        }
    }
}
