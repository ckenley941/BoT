﻿using BucketOfThoughts.Services.Languages;
using BucketOfThoughts.Services.Languages.Objects;
using BucketOfThoughts.Services.Thoughts;
using BucketOfThoughts.Services.Thoughts.Objects;

namespace BucketOfThoughts.Api.Handlers.Thoughts
{
    public class GetThoughtByIdHandler
    {
        protected readonly ThoughtsService _service;
        public GetThoughtByIdHandler(ThoughtsService service)
        {
            _service = service;
        }
        public async Task<ThoughtDto> HandleAsync(int id)
        {
            return await _service.GetByIdAsync(id);
        }
    }
}
