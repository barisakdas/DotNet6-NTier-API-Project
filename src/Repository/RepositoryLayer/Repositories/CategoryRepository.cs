namespace RepositoryLayer.Repositories;
public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Category> GetSingleCategoryWithProducts(int categoryId)
    {
        return await _context.Categories.Include(x => x.Products).Where(x => x.ID == categoryId).SingleOrDefaultAsync();
    }
}

