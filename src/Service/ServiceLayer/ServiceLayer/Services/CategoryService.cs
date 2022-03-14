namespace ServiceLayer.Services;
public class CategoryService : Service<Category>, ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(IGenericRepository<Category> repository, IUnitOfWorks unitOfWorks, ICategoryRepository categoryService, IMapper mapper)
        : base(repository, unitOfWorks) => (_categoryRepository, _mapper) = (categoryService, mapper);

    public async Task<CustomResponseDto<CategoryWithProductsDto>> GetSingleCategoryWithProducts(int categoryId)
    {
        var category = await _categoryRepository.GetSingleCategoryWithProducts(categoryId);
        return CustomResponseDto<CategoryWithProductsDto>.Success(200, _mapper.Map<CategoryWithProductsDto>(category));
    }
}

