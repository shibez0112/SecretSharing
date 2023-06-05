using AutoMapper;
using SecretSharing.Core.Entities;
using SecretSharing.Dtos;

namespace SecretSharing.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserFile, UserFileDto>().ReverseMap();
        }
    }
}
