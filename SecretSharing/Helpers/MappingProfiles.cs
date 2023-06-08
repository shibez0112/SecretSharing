using AutoMapper;
using SecretSharing.Core.Entities;
using SecretSharing.Dtos;

namespace SecretSharing.Helpers
{
    // AutoMapper config for mapping objects
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserFile, UserFileDto>().ReverseMap();
            CreateMap<UserText, UserTextDto>().ReverseMap();
        }
    }
}
