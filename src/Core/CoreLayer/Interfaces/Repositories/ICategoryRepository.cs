namespace CoreLayer.Interfaces.Repositories;
public interface ICategoryRepository
{
    Task<Category> GetSingleCategoryWithProducts(int id);
}

