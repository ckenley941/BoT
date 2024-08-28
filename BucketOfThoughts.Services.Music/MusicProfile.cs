using AutoMapper;
using BucketOfThoughts.Services.Music.Data;
using BucketOfThoughts.Services.Music.Objects;

namespace BucketOfThoughts.Services.Music
{
    public class MusicProfile : Profile
    {
        public MusicProfile()
        {
            CreateMap<Concert, ConcertDto>()
                .ForMember(dest => dest.BandName, opt => opt.MapFrom(src => src.Band.Name))
                .ForMember(dest => dest.Venue, opt => opt.MapFrom(src => src.Venue!.Name))
              .ReverseMap();
            CreateMap<SetlistSong, SetlistSongDto>().ReverseMap();
        }
    }
}
