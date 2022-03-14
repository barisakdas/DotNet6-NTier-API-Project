namespace RepositoryLayer.Repositories;
public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<Product>> GetProductsWithCategory()
    {
        return await _context.Products.Include(p => p.Category).ToListAsync();
    }
}
