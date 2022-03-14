namespace CoreLayer.Interfaces.Services;
public interface ICategoryService : IService<Category>
{
    Task<CustomResponseDto<CategoryWithProductsDto>> GetSingleCategoryWithProducts(int categoryId);
}

