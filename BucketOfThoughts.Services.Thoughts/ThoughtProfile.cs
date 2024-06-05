using AutoMapper;
using BucketOfThoughts.Services.Thoughts.Data;
using BucketOfThoughts.Services.Thoughts.Objects;

namespace BucketOfThoughts.Services.Thoughts
{
    public class ThoughtProfile : Profile
    {
        public ThoughtProfile()
        {
            CreateMap<Thought, ThoughtDto>()
                .ForMember(dest => dest.ThoughtDateTime, opt => opt.MapFrom(src => src.CreatedDateTime))
                .ForMember(dest => dest.Bucket, opt => opt.MapFrom(src => src.ThoughtBucket))
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.ThoughtDetails))
                .ReverseMap();

            CreateMap<Thought, ThoughtGridDto>()
                .ForMember(dest => dest.Bucket, opt => opt.MapFrom(src => src.ThoughtBucket.Description))
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => string.Join(", ", src.ThoughtDetails.Select(y => y.Description).ToList())));

            CreateMap<ThoughtBucket, ThoughtBucketDto>().ReverseMap();
            CreateMap<ThoughtDetail, ThoughtDetailDto>().ReverseMap();

            CreateMap<OutdoorActivityLogDto, OutdoorActivityLog>()
                .ForMember(dest => dest.ActivityTime, opt => opt.MapFrom(src => new TimeSpan(src.ActivityTimeHours ?? 0, src.ActivityTimeMinutes ?? 0, 0) { }));

            CreateMap<OutdoorActivityLog, OutdoorActivityLogDto>()
                .ForMember(dest => dest.ActivityTimeHours, opt => 
                {
                    opt.PreCondition(src => (src.ActivityTime.HasValue));
                    opt.MapFrom(src => src.ActivityTime.Value.Hours);
                })
                .ForMember(dest => dest.ActivityTimeMinutes, opt =>
                {
                    opt.PreCondition(src => (src.ActivityTime.HasValue));
                    opt.MapFrom(src => src.ActivityTime.Value.Minutes);
                });
        }
    }
}
