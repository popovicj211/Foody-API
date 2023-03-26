using Application.DataTransfer;
using AutoMapper;
using Domain.Entities;

namespace Implementation.AutoMapper
{
    public class ConfigurationMapping : Profile
    {
        public ConfigurationMapping()
        {
            CreateMap<RoleEntity, RoleDTO>().ReverseMap();
            CreateMap<UserEntity, UserDTO>().ForMember(dest =>
            dest.FirstName,
            opt => opt.MapFrom(src => src.FirstName)
            )
             .ForMember(dest =>
            dest.LastName,
            opt => opt.MapFrom(src => src.LastName))
              .ForMember(dest =>
               dest.Username,
               opt => opt.MapFrom(src => src.Username))
               .ForMember(dest =>
            dest.Password,
            opt => opt.MapFrom(src => src.Password))
                .ForMember(dest =>
            dest.Email,
            opt => opt.MapFrom(src => src.Email))
                 .ForMember(dest =>
            dest.Role,
            opt => opt.MapFrom(src => src.Role))
                   .ForMember(dest =>
            dest.RoleId,
            opt => opt.MapFrom(src => src.RoleId))
             .ReverseMap();
        }
    }
}
