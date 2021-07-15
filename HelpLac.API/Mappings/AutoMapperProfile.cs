using AutoMapper;
using HelpLac.API.Models.Request;
using HelpLac.API.Models.Response;
using HelpLac.Domain.Dtos;
using HelpLac.Domain.Entities.Identity;

namespace HelpLac.API.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserLoginRequest>().ReverseMap();
            CreateMap<User, UserRegisterRequest>().ReverseMap();
            CreateMap<User, UserResponse>().ReverseMap();

            CreateMap<PaginationRequest, PaginationRequestDto>().ReverseMap();
            CreateMap<ProductRequest, ProductDto>().ReverseMap();
        }
    }
}
