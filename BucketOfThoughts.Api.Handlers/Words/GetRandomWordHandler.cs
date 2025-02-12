﻿using BucketOfThoughts.Services.Languages;
using BucketOfThoughts.Services.Languages.Objects;

namespace BucketOfThoughts.Api.Handlers.Words
{
    public class GetRandomWordHandler 
    {
        protected readonly WordsService _service;
        public GetRandomWordHandler(WordsService service)
        {
            _service = service;
        }
        public async Task<WordTranslationDto> HandleAsync()
        {
            return await _service.GetRandomWordAsync();
        }
    }
}
