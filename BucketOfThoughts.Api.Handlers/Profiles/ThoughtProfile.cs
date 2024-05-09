using AutoMapper;
using BucketOfThoughts.Services.Thoughts.Data;
using BucketOfThoughts.Services.Thoughts.Objects;

namespace BucketOfThoughts.Api.Handlers.Profiles
{
    public class ThoughtProfile : Profile
    {
        public ThoughtProfile()
        {
            CreateMap<Thought, ThoughtDto>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.ThoughtDateTime, opt => opt.MapFrom(src => src.CreatedDateTime))
                .ReverseMap();

            CreateMap<ThoughtCategory, ThoughtCategoryDto>().ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<ThoughtDetail, ThoughtDetailDto>().ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        }
    }
}
