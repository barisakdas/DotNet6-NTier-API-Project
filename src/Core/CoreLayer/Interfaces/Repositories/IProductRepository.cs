
namespace CoreLayer.Interfaces.Repositories;
public interface IProductRepository : IGenericRepository<Product>
{
    Task<List<Product>> GetProductsWithCategory();

}
