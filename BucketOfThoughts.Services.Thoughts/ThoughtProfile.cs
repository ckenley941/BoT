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
        }
    }
}
