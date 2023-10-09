using Common.Data.Objects.Words;
using BucketOfThoughts.Api.Services;
using EnsenaMe.Data.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace BucketOfThoughts.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordsController : ControllerBase
    {
        private readonly IWordsService _wordsService;

        public WordsController(IWordsService wordsService)
        {
            _wordsService = wordsService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WordTranslationDto>> GetById(int id)
        {
            WordTranslationDto word = await _wordsService.GetByIdAsync(id);
            return word;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(InsertWordCardDto insertedWord)
        {
            int wordId = await _wordsService.AddOrUpdateWordCardAsync(insertedWord);
            return wordId;
        }

        [HttpGet("GetRandomWord")]
        public async Task<ActionResult<WordTranslationDto>> GetRandomWord()
        {
            WordTranslationDto word = await _wordsService.GetRandomWordAsync();
            return word;
        }


        [HttpGet("GetTranslations/{id}")]
        public async Task<ActionResult<List<WordDto>>> GetTranslations(int id)
        {
            List<WordDto> wordTranslations = await _wordsService.GetTranslationsAsync(id);
            return Ok(wordTranslations);
        }

        [HttpGet("GetTranslationsWithContext/{id}")]
        public async Task<ActionResult<List<WordContextDto>>> GetTranslationsWithContext(int id)
        {
            var wordContexts = await _wordsService.GetTranslationsWithContextAsync(id);
            return Ok(wordContexts);
        }

        [HttpGet("GetWordRelationships/{id}")]
        public async Task<ActionResult<List<WordTranslationDto>>> GetWordRelationships(int id)
        {
            var words = await _wordsService.GetWordRelationshipsAsync(id);
            return Ok(words);
        }
    }
}
