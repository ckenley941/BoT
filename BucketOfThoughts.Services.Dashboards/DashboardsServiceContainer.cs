﻿using AutoMapper;
using BucketOfThoughts.Core.Infrastructure.Interfaces;
using BucketOfThoughts.Services.Languages;
using BucketOfThoughts.Services.Languages.Data;
using BucketOfThoughts.Services.Thoughts;
using BucketOfThoughts.Services.Thoughts.Data;
using Microsoft.Extensions.Caching.Distributed;

namespace BucketOfThoughts.Service.Dashboards
{
    public class DashboardsServiceContainer
    {
        private readonly Lazy<WordsService> _wordsService;
        private readonly Lazy<ThoughtsService> _thoughtsService;
        private readonly Lazy<OutdoorActivityLogService> _outdoorActivityLogService;
        public WordsService WordsService { get { return _wordsService.Value; } }
        public ThoughtsService ThoughtsService { get { return _thoughtsService.Value; } }
        public OutdoorActivityLogService OutdoorActivityLogService { get { return _outdoorActivityLogService.Value; } }
        public DashboardsServiceContainer(IDistributedCache cache, IMapper mapper, LanguageDbContext languageDbContext, IThoughtsRepository thoughtsRepository, IOutdoorActivityLogRepository outdoorActivityLogRepository)
        {
            _wordsService = new(() => { return new WordsService(languageDbContext); });
            _thoughtsService = new(() => { return new ThoughtsService(thoughtsRepository, cache, mapper); });
            _outdoorActivityLogService = new(() => { return new OutdoorActivityLogService(outdoorActivityLogRepository, cache, mapper); });
        }

    }
}
