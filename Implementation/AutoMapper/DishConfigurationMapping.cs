using Application.DataTransfer;
using AutoMapper;
using Domain.Entities;

namespace Implementation.AutoMapper
{
    public class DishConfigurationMapping : Profile
    {
        public DishConfigurationMapping()
        {
            CreateMap<DishEntity, DishDTO>().ForMember(dest =>
            dest.Name,
                      opt => opt.MapFrom(src => src.Name))
                                .ForMember(dest =>
            dest.Description,
                     opt => opt.MapFrom(src => src.Description))
                               .ForMember(dest =>
            dest.Price,
                     opt => opt.MapFrom(src => src.Price))
                              .ForMember(dest =>
           dest.ImagePath,
                     opt => opt.MapFrom(src => src.ImagePath))
                              .ReverseMap();
        }
    }
}
