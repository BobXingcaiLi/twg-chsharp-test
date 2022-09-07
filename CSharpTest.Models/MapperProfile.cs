using AutoMapper;

namespace CSharpTest.Models
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ProductObj, ProductModel>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => $"R{src.ProductKey}"));
                

            CreateMap<ProductObj, ProductWithPriceModel>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => $"R{src.ProductKey}"));
        }
    }
}
