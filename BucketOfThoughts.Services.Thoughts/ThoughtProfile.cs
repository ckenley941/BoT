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
                .ForMember(dest => dest.TotalTime, opt => opt.MapFrom(src => new TimeSpan(src.TotalTimeHours ?? 0, src.TotalTimeMinutes ?? 0, 0) { }))
                .ForMember(dest => dest.MovingTime, opt => opt.MapFrom(src => new TimeSpan(src.MovingTimeHours ?? 0, src.MovingTimeMinutes ?? 0, 0) { }));

            CreateMap<OutdoorActivityLog, OutdoorActivityLogDto>()
                .ForMember(dest => dest.TotalTimeHours, opt => 
                {
                    opt.PreCondition(src => (src.TotalTime.HasValue));
                    opt.MapFrom(src => src.TotalTime.Value.Hours);
                })
                .ForMember(dest => dest.TotalTimeMinutes, opt =>
                {
                    opt.PreCondition(src => (src.TotalTime.HasValue));
                    opt.MapFrom(src => src.TotalTime.Value.Minutes);
                })
                .ForMember(dest => dest.MovingTimeHours, opt =>
                 {
                     opt.PreCondition(src => (src.MovingTime.HasValue));
                     opt.MapFrom(src => src.MovingTime.Value.Hours);
                 })
                .ForMember(dest => dest.MovingTimeMinutes, opt =>
                {
                    opt.PreCondition(src => (src.MovingTime.HasValue));
                    opt.MapFrom(src => src.MovingTime.Value.Minutes);
                });
        }
    }
}
