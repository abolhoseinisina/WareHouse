using Application.Model;
using AutoMapper;
using Domain.Model;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Article, ArticleDTO>()
                .ForMember(a => a.ArticleId, opt => opt.MapFrom(itm => itm.Id)); ;
            CreateMap<Product, ProductDTO>();
            CreateMap<ArticleProduct, ArticleProductDTO>();

            CreateMap<ArticleDTO, Article>()
                .ForMember(a => a.Id, opt => opt.MapFrom(itm => itm.ArticleId)); ;
            CreateMap<ProductDTO, Product>();
            CreateMap<ArticleProductDTO, ArticleProduct>();
        }
    }
}
