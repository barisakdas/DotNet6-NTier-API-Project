

namespace ServiceLayer.Mapping;
public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap(); ;
        CreateMap<Product, ProductUpdateDto>().ReverseMap();
        CreateMap<Product, ProductCreateDto>().ReverseMap();
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Category, CategoryUpdateDto>().ReverseMap();
        CreateMap<Category, CategoryCreateDto>().ReverseMap();
        CreateMap<ProductFeatureDto, ProductFeatureDto>().ReverseMap();
    }
}
