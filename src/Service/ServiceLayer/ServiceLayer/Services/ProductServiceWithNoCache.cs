namespace ServiceLayer.Services;
public class ProductServiceWithNoCache : Service<Product>, IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductServiceWithNoCache(IProductRepository productRepository, IMapper mapper, IGenericRepository<Product> repository, IUnitOfWorks unitOfWorks)
        : base(repository, unitOfWorks) => (_productRepository, _mapper) = (productRepository, mapper);

    public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
    {
        var product = await _productRepository.GetProductsWithCategory();
        var productsDto = _mapper.Map<List<ProductWithCategoryDto>>(product);

        return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productsDto);
    }
}
