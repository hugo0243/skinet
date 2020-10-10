using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(prodToReturn => prodToReturn.ProductBrand, o => o.MapFrom(prod => prod.ProductBrand.Name))
                .ForMember(pToReturn => pToReturn.ProductType, opts => opts.MapFrom(prod=> prod.ProductType.Name))
                .ForMember(pToReturn => pToReturn.PictureUrl, opts => opts.MapFrom<ProductUrlResolver>());
        }
    }
}