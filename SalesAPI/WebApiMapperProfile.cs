using AutoMapper;
using SalesApi.Domain.Models;
using SalesAPI.WebApi.Dto;

namespace SalesAPI.WebApi
{
    public class WebApiMapperProfile : Profile
    {
        public WebApiMapperProfile()
        {
            CreateMap<SalePost, Sale>();
            CreateMap<ItemPostDto, Item>();
            CreateMap<SalesmanPostDto, Salesman>();

        }
    }
}
