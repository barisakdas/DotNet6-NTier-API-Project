namespace ApiLayer.Controllers;

public class ProductController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IProductService _service;

    public ProductController(IMapper mapper, IProductService service) => (_mapper, _service) = (mapper, service);


    [HttpGet("GetProductsWithCategory")]
    public async Task<IActionResult> GetProductsWithCategory()
    {
        return CreateActionResult(await _service.GetProductsWithCategory());
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var products = await _service.GetAll();
        var productDtos = _mapper.Map<List<ProductDto>>(products.ToList());

        return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productDtos));
    }

    [ServiceFilter(typeof(NotFoundFilter<Product>))]    // Bu filterı normal bir attribute olarak kullanamazsınız. Constructor da parametre alan bir filter veya attribute kullanacağınız zaman ServiceFilter üzerinden kullanmalısınız.
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _service.GetByIdAsync(id);
        var productDto = _mapper.Map<ProductDto>(product);

        return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productDto));
    }

    [HttpPost]
    public async Task<IActionResult> Save(ProductCreateDto productDto)
    {
        var product = await _service.AddAsync(_mapper.Map<Product>(productDto));
        var response = _mapper.Map<ProductDto>(product);

        return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, response));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductUpdateDto productDto)
    {
        await _service.UpdateAsync(_mapper.Map<Product>(productDto));

        return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
    }

    [ServiceFilter(typeof(NotFoundFilter<Product>))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _service.GetByIdAsync(id);
        await _service.DeleteAsync(product);

        return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
    }
}

