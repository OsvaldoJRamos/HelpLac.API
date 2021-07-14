using AutoMapper;
using HelpLac.API.Models.Request;
using HelpLac.Domain.Dtos;

namespace HelpLac.API.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PaginationRequest, PaginationRequestDto>().ReverseMap();
            CreateMap<ProductRequest, ProductDto>().ReverseMap();
        }
    }
}
