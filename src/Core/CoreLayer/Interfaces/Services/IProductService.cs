namespace CoreLayer.Interfaces.Services;
public interface IProductService : IService<Product>
{
    Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory();
}

