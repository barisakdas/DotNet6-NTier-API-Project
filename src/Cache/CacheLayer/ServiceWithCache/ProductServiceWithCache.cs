namespace CacheLayer.ServiceWithCache;
public class ProductServiceWithCache : IProductService
{
    private const string CacheProductKey = "productsCache";
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWorks _unitOfWorks;

    public ProductServiceWithCache(IMapper mapper, IMemoryCache memoryCache, IProductRepository productRepository, IUnitOfWorks unitOfWorks)
    {
        _mapper = mapper;
        _memoryCache = memoryCache;
        _productRepository = productRepository;
        _unitOfWorks = unitOfWorks;

        /* TryGetValue geriye bool dönen, datanın olup olmadığını kontrol eden bir fonksiyon. 
         * Eğer ki data mevcutsa out parametresinin yanına o datanın atanacağı değişkeni yazarız. 
         * Değişkeni veya değeri istemiyorsak `_` koyarak o datayı istemediğimizi belirtiriz. Boşune memory de yer tutmamış oluruz.
         */
        if (!_memoryCache.TryGetValue(CacheProductKey, out _))
        {
            _memoryCache.Set(CacheProductKey, _productRepository.GetAll().ToList());
        }
    }

    public async Task<Product> AddAsync(Product entity)
    {
        await _productRepository.AddAsync(entity);
        await _unitOfWorks.CommitAsync();
        await CacheAllProducts();   // Eklendikten sonra tüm datayı tekrar cache memory sine ekliyoruz.
        return entity;
    }

    public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
    {
        await _productRepository.AddRangeAsync(entities);
        await _unitOfWorks.CommitAsync();
        await CacheAllProducts();   // Eklendikten sonra tüm datayı tekrar cache memory sine ekliyoruz.
        return entities;
    }

    public Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Product entity)
    {
        _productRepository.Delete(entity);
        await _unitOfWorks.CommitAsync();
        await CacheAllProducts();   // Eklendikten sonra tüm datayı tekrar cache memory sine ekliyoruz.
    }

    public async Task DeleteRangeAsync(IEnumerable<Product> entities)
    {
        _productRepository.DeleteRange(entities);
        await _unitOfWorks.CommitAsync();
        await CacheAllProducts();   // Eklendikten sonra tüm datayı tekrar cache memory sine ekliyoruz.
    }

    public Task<IEnumerable<Product>> GetAll()
    {
        return Task.FromResult(_memoryCache.Get<IEnumerable<Product>>(CacheProductKey));
    }

    public Task<Product> GetByIdAsync(int id)
    {
        var product = _memoryCache.Get<List<Product>>(CacheProductKey).FirstOrDefault(x => x.ID == id);
        if (product is null)
            throw new NotFoundException($"{typeof(Product).Name}({id}) not found");

        return Task.FromResult(product);
    }

    // Bu fonksiyon çok az kullanılacağı için cache içinden gelmeyecek.
    public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
    {
        var products = await _productRepository.GetProductsWithCategory();
        var productsWithCategory = _mapper.Map<List<ProductWithCategoryDto>>(products);

        return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productsWithCategory);
    }

    public async Task UpdateAsync(Product entity)
    {
        _productRepository.Update(entity);
        await _unitOfWorks.CommitAsync();
        await CacheAllProducts();
    }

    public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
    {
        return _memoryCache.Get<List<Product>>(CacheProductKey).Where(expression.Compile()).AsQueryable();
    }

    public async Task CacheAllProducts()
    {
        _memoryCache.Set(CacheProductKey, _productRepository.GetAll().ToList());
    }
}

