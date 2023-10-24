﻿using BucketOfThoughts.Services.Thoughts.Data;
using BucketOfThoughts.Services.Thoughts;

namespace BucketOfThoughts.Api.Handlers.Thoughts
{
    public class GetRandomThoughtHandler
    {
        protected readonly ThoughtsService _service;
        public GetRandomThoughtHandler(ThoughtsService service)
        {
            _service = service;
        }
        public async Task<Thought> HandleAsync()
        {
            return await _service.GetRandomThoughtAsync();
        }
    }
}
